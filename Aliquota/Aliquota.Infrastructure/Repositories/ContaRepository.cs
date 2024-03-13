using Aliquota.Domain.Entities;
using Aliquota.Domain.Interfaces.Repositories;

namespace Aliquota.Infrastructure.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly AppDbContext _context;

        public ContaRepository(AppDbContext context)
        {
            _context = context;
        }

        public ContaEntity Depositar(ContaEntity conta)
        {
            var contaEntity = _context.ContaEntity.FirstOrDefault(x => x.Id == conta.Id);

            contaEntity.Saldo = conta.Saldo;
            _context.SaveChanges();

            return contaEntity;
        }

        public ContaEntity Get(int id)
        {
            return _context.ContaEntity.FirstOrDefault(x => x.Id == id);
        }

        public ContaEntity GetByNumero(int numero)
        {
            return _context.ContaEntity.FirstOrDefault(x => x.Numero == numero);
        }

        public IEnumerable<int> GetNumerosConta()
        {
            return _context.ContaEntity.Select(x => x.Numero);
        }

        public ContaEntity ValorAplicado(int id, double valAplicado, double valSaldo)
        {
            var contaEntity = _context.ContaEntity.FirstOrDefault(x => x.Id == id);
            contaEntity.ValorAplicado = valAplicado;
            contaEntity.Saldo = valSaldo;

            _context.SaveChanges();

            return contaEntity; 
        }
    }
}
