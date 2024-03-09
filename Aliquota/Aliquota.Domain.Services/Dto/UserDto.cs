using Aliquota.Domain.Dto;

namespace Aliquota.Domain.Dto
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public ContaDto Conta { get; set; }
    }
}
