using Aliquota.Domain.Dto;

namespace Aliquota.API.Models
{
    public class ContaModel
    {
        public int Numero { get; set; }
        public double Saldo { get; set; }

        public static ContaDto ConvertToDto(ContaModel conta)
        {
            var dto = new ContaDto()
            {
                Numero = conta.Numero,
                Saldo = conta.Saldo
            };

            return dto;
        }
    }
}
