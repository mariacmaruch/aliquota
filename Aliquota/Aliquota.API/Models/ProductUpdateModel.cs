using Aliquota.Domain.Dto;

namespace Aliquota.API.Models
{
    public class ProductUpdateModel
    {
        public DateTime DtResgate { get; set; }

        public static ProductDto ConvertToDto(ProductUpdateModel produto)
        {
            var dto = new ProductDto()
            {
                DtResgate = produto.DtResgate
            };

            return dto;
        }

    }
}
