using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces.Base;

namespace ControleFacil.src.Domain.Interfaces
{
    public interface IFuncionariosRepository : IBaseRepository<Funcionarios>
    {
        Task<Funcionarios?> Login(int nif, CancellationToken ct);
    }
}