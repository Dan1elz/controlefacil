using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.src.Infrastructure.Repositories
{
    public class FuncionariosRepository(ApplicationDbContext context) : BaseRepository<Funcionarios>(context), IFuncionariosRepository
    { 
        public virtual async Task<Funcionarios?> Login(int nif, CancellationToken ct)
        {
            return await _context.Funcionarios.SingleOrDefaultAsync(predicate: u => u.Nif == nif, ct);
        }
    }
}