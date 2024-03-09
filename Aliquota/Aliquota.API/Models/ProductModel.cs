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
            var dtoConta = new ContaDto()
            {
                Id = produto.IdConta
            };

            var dto = new ProductDto()
            {
                DtAplicacao = produto.DtAplicacao, 
                Valor = produto.Valor, 
                Conta = dtoConta
            };

            return dto;
        }
    }
}
