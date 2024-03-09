using Aliquota.Domain.Constants;
using Aliquota.Domain.Dto;
using Aliquota.Domain.Entities;
using Aliquota.Domain.Interfaces.Repositories;
using Aliquota.Domain.Interfaces.Services;
using Aliquota.Domain.Services.Exceptions;
using AutoMapper;

namespace Aliquota.Domain.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IContaService _contaService; 
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IContaService contaService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _contaService = contaService;
        }

        private void ValidateConta(int id)
        {
            if (_contaService.Get(id) == null)
                throw new InvalidContaException("Conta não encontrada.");
        }

        public ProductDto Aplicar(ProductDto product)
        {
            ValidateConta(product.Conta.Id);

            if(product.Valor <= 0)
            {
                throw new InvalidProductException("Valor aplicado não pode ser menor ou igual a zero.");
            }

            var conta = _contaService.Get(product.Conta.Id);

            if(conta.Saldo < product.Valor)
            {
                throw new InvalidProductException("Não é possível aplicar o valor, pois o saldo disponível é insuficiente.");
            }

            var produtoEntity = _mapper.Map<ProductEntity>(product);

            var aplicado = _productRepository.Aplicar(produtoEntity);

            return _mapper.Map<ProductDto>(aplicado);
        }

        private void ValidateProduct(int id)
        {
            if (!_productRepository.GetAplicacoes().Any(x => x.Id == id))
                throw new InvalidProductException("Aplicação não encontrada.");
        }

        private double CalculateProfit(int meses, double valor)
        {
            return valor * Math.Pow(1 + ProductConstant.Rentabilidade, meses);
        }

        public ProductDto Resgatar(int id, ProductDto product)
        {
            ValidateProduct(id);

            var produtoEntity = _productRepository.Get(id);

            if (!produtoEntity.Ativo)
            {
                throw new InvalidProductException("Produto já resgatado.");
            }

            var compare = product.DtResgate - produtoEntity.DtAplicacao;
            var time = compare.Days / 365;
            var timeMonths = (int)time * 12;

            var valorAcumulado = CalculateProfit(timeMonths, produtoEntity.Valor);

            double newValue = 0;

            if (time <= 1)
            {
                newValue = (22.5 / 100) * valorAcumulado;
            } 
            else if(time > 1 && time <= 2)
            {
                newValue = (18.5 / 100) * valorAcumulado;
            } 
            else if(time > 2)
            {
                newValue = (15.0 / 100) * valorAcumulado;
            }

            product.Valor = valorAcumulado - newValue;
            product.Id = id;

            var entidade = _mapper.Map<ProductEntity>(product);

            var resgatar = _productRepository.Resgatar(entidade);

            var updateConta = _contaService.Get(product.Conta.Id);
            updateConta.Saldo = valorAcumulado - newValue;
            _contaService.Depositar(updateConta.Id, updateConta);

            return _mapper.Map<ProductDto>(resgatar);
        }


        public IEnumerable<ProductDto> GetAplicacoes()
        {
            var produtos = _productRepository.GetAplicacoes();
            var produtosDto = new List<ProductDto>();

            foreach(var item in produtos)
            {
                var dto = _mapper.Map<ProductDto>(item);

                produtosDto.Add(dto);
            }

            return produtosDto;
        }

        public IEnumerable<ProductDto> GetResgastes()
        {
            var produtos = _productRepository.GetResgastes();
            var produtoDto = new List<ProductDto>();

            foreach(var item in produtos)
            {
                var dto = _mapper.Map<ProductDto>(item);

                produtoDto.Add(dto);
            }

            return produtoDto;
        }

        public ProductDto Get(int id)
        {
            var produto = _productRepository.Get(id);

            return _mapper.Map<ProductDto>(produto);
        }
    }
}
