using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        ProductEntity Aplicar(ProductEntity product);
        ProductEntity Resgatar(ProductEntity product);
        ProductEntity Get(int id);
        IEnumerable<ProductEntity> GetAplicacoes();
        IEnumerable<ProductEntity> GetResgastes();
    }
}
