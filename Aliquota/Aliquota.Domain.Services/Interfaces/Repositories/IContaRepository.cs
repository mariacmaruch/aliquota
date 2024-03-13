using Aliquota.Domain.Entities;

namespace Aliquota.Domain.Interfaces.Repositories
{
    public interface IContaRepository
    {
        ContaEntity Get(int id);
        ContaEntity Depositar(ContaEntity conta);
        ContaEntity ValorAplicado(int id, double valAplicado, double valSaldo);
        IEnumerable<int> GetNumerosConta();
        ContaEntity GetByNumero(int numero);
    }
}
