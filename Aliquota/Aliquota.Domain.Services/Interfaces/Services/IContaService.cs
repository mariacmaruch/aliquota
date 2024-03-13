using Aliquota.Domain.Dto;

namespace Aliquota.Domain.Interfaces.Services
{
    public interface IContaService
    {
        ContaDto Get(int id);
        ContaDto Depositar(ContaDto conta);
    }
}
