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
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IContaRepository contaRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _contaRepository = contaRepository;
        }

        private void ValidateConta(int id)
        {
            if (_contaRepository.Get(id) == null)
                throw new InvalidContaException("Conta não encontrada.");
        }

        public ProductDto Aplicar(ProductDto product)
        {
            ValidateConta(product.IdConta);

            if(product.Valor <= 0)
            {
                throw new InvalidProductException("Valor aplicado não pode ser menor ou igual a zero.");
            }

            var conta = _contaRepository.Get(product.IdConta);

            if (conta.Saldo < product.Valor)
            {
                throw new InvalidProductException("Não é possível aplicar o valor, pois o saldo disponível é insuficiente.");
            }

            conta.ValorAplicado += product.Valor;
            conta.Saldo -= product.Valor;

            _contaRepository.ValorAplicado(product.IdConta, conta.ValorAplicado, conta.Saldo);

            var produtoEntity = _mapper.Map<ProductEntity>(product);

            var aplicado = _productRepository.Aplicar(produtoEntity);

            return _mapper.Map<ProductDto>(aplicado);
        }

        private void ValidateProduct(int id)
        {
            if (_productRepository.Get(id) == null)
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

            if(produtoEntity.DtAplicacao > product.DtResgate)
            {
                throw new InvalidProductException("Data de resgate menor do que a de aplicação.");
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

            var updateConta = _contaRepository.Get(produtoEntity.IdConta);
            updateConta.ValorAplicado -= produtoEntity.Valor;
            updateConta.Saldo += valorAcumulado - newValue;
            _contaRepository.Depositar(updateConta);

            var entidade = _mapper.Map<ProductEntity>(product);

            var resgatar = _productRepository.Resgatar(entidade);

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
