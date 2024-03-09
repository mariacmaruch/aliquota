using Aliquota.Domain.Dto;

namespace Aliquota.Domain.Interfaces.Services
{
    public interface IProductService
    {
        ProductDto Aplicar(ProductDto product);
        ProductDto Resgatar(int id, ProductDto product);
        ProductDto Get(int id);
        IEnumerable<ProductDto> GetAplicacoes();
        IEnumerable<ProductDto> GetResgastes();

    }
}
