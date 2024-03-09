using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        UserEntity Create(UserEntity user);
        UserEntity Update(UserEntity user);
        IEnumerable<UserEntity> GetAll();
        UserEntity Get(int id);
        IEnumerable<string> GetEmail();

    }
}
