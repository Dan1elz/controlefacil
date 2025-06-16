using ControleFacil.src.Domain.Entities;

namespace ControleFacil.src.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task Create(RefreshToken token, CancellationToken ct);
        Task Delete(RefreshToken token, CancellationToken ct);
        Task<RefreshToken?> GetToken(string value, CancellationToken ct);
        Task<RefreshToken?> GetTokenByID(int Id, CancellationToken ct);
        Task<RefreshToken?> GetTokenByFuncionarioID(int Id, CancellationToken ct);
    }
}