using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.src.Infrastructure.Repositories
{
    public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context = context;

        public virtual async Task Create(RefreshToken token, CancellationToken ct)
        {
            await _context.RefreshTokens.AddAsync(token, ct);
            await _context.SaveChangesAsync(ct);
        }
        public virtual async Task Delete(RefreshToken token, CancellationToken ct)
        {
            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync(ct);
        }
        public virtual async Task<RefreshToken?> GetToken(string value, CancellationToken ct)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(predicate: u => u.Value == value, ct);
        }
        public virtual async Task<RefreshToken?> GetTokenByID(int Id, CancellationToken ct)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(predicate: u => u.Id == Id, ct);
        }
        public virtual async Task<RefreshToken?> GetTokenByFuncionarioID(int Id, CancellationToken ct)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(predicate: u => u.FuncionarioId == Id, ct);
        }
    }
}
