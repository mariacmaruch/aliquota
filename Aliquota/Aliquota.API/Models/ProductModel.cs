using Aliquota.Domain.Dto;

namespace Aliquota.API.Models
{
    public class ProductModel
    {
        public int IdConta { get; set; }
        public DateTime DtAplicacao { get; set; }
        public double Valor { get; set; }

        public static ProductDto ConvertToDto(ProductModel produto)
        {
            var dto = new ProductDto()
            {
                DtAplicacao = produto.DtAplicacao, 
                DtResgate = DateTime.MinValue,
                Valor = produto.Valor, 
                IdConta = produto.IdConta, 
                Ativo = true
            };

            return dto;
        }
    }
}
