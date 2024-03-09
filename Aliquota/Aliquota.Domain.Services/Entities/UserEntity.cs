using Aliquota.Domain.Entities.Base;

namespace Aliquota.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public int IdConta { get; set; }
        public virtual ContaEntity Conta { get; set; }
    }
}
