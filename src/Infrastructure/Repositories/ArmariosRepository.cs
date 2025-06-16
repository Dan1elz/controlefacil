using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Infrastructure.Repositories.Base;

namespace ControleFacil.src.Infrastructure.Repositories
{
    public class ArmariosRepository(ApplicationDbContext context) : BaseRepository<Armarios>(context), IArmariosRepository
    { }
}