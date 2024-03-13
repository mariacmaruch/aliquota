using Aliquota.Domain.Entities;
using Aliquota.Domain.Interfaces.Repositories;

namespace Aliquota.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public UserEntity Create(UserEntity user)
        {
            _context.UserEntity.Add(user);
            _context.SaveChanges();

            return user;
        }

        public UserEntity Get(int id)
        {
            return _context.UserEntity.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UserEntity> GetAll()
        {
            return _context.UserEntity.ToList();
        }

        public IEnumerable<string> GetEmail()
        {
            return _context.UserEntity.Select(x => x.Email).ToList();
        }

        public UserEntity Update(UserEntity user)
        {
            var usuario = _context.UserEntity.FirstOrDefault(x => x.Id == user.Id);

            usuario.Nome = user.Nome;
            usuario.Cpf = user.Cpf;
            usuario.Email = user.Email;

            _context.SaveChanges();

            return usuario;

        }
    }
}
