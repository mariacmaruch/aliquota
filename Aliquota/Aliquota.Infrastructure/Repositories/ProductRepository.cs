using Aliquota.Domain.Entities;
using Aliquota.Domain.Interfaces.Repositories;

namespace Aliquota.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public ProductEntity Aplicar(ProductEntity product)
        {
            _context.ProductEntity.Add(product);
            _context.SaveChanges();

            return product;
        }

        public ProductEntity Resgatar(ProductEntity product)
        {
            var produto = _context.ProductEntity.FirstOrDefault(x => x.Id == product.Id);
            
            produto.Ativo = false;
            produto.DtResgate = product.DtResgate;
            produto.Valor = product.Valor;

            _context.SaveChanges();

            return product;
        }

        public ProductEntity Get(int id)
        {
            return _context.ProductEntity.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ProductEntity> GetAplicacoes()
        {
            return _context.ProductEntity.Where(x => x.Ativo);
        }

        public IEnumerable<ProductEntity> GetResgastes()
        {
            return _context.ProductEntity.Where(x => !x.Ativo);
        }
    }
}
