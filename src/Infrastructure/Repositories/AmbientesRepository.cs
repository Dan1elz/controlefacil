using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Infrastructure.Repositories.Base;

namespace ControleFacil.src.Infrastructure.Repositories
{
    public class AmbientesRepository(ApplicationDbContext context) : BaseRepository<Ambientes>(context), IAmbientesRepository
    { }
}