using Aliquota.Domain.Dto;

namespace Aliquota.API.Models
{
    public class UserModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public ContaModel Conta { get; set; }

        public static UserDto ConvertToDto(UserModel user, ContaModel conta) 
        {
            if (user == null) return null;

            var contaDto = new ContaDto()
            {
                Numero = conta.Numero,
                Saldo = conta.Saldo
            };

            var dto = new UserDto()
            {
                Nome = user.Nome,
                Email = user.Email,
                Cpf = user.Cpf,
                Conta = contaDto
            };

            return dto;
        }
    }
}
