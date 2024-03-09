using Aliquota.Domain.Dto;

namespace Aliquota.Domain.Interfaces.Services
{
    public interface IUserService
    {
        UserDto Create(UserDto user); 
        UserDto Update(int id, UserDto user);
        List<UserDto> GetAll();
        UserDto Get(int id);
    }
}
