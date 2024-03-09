using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Interfaces.Repositories
{
    public interface IContaRepository
    {
        ContaEntity Get(int id);
        ContaEntity Depositar(ContaEntity conta);
        IEnumerable<int> GetNumerosConta();

    }
}
