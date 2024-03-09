using Aliquota.Domain.Dto;

namespace Aliquota.API.Models
{
    public class UserUpdateModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }

        public static UserDto CovertToDto(UserUpdateModel user)
        {
            if (user == null) return null;

            var dto = new UserDto()
            {
                Nome = user.Nome,
                Email = user.Email,
                Cpf = user.Cpf
            };

            return dto;
        }
    }
}
