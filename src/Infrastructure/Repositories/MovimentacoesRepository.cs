using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Infrastructure.Repositories.Base;

namespace ControleFacil.src.Infrastructure.Repositories
{
    public class MovimentacoesRepository(ApplicationDbContext context) : BaseRepository<Movimentacoes>(context), IMovimentacoesRepository
    { }
}