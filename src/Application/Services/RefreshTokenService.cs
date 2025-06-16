using ControleFacil.src.Application.Exceptions;
using ControleFacil.src.Application.Utils;
using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces;

namespace ControleFacil.src.Application.Services
{
    public class RefreshTokenService(IRefreshTokenRepository repository, JwtUtils jwt)
    {
        private readonly IRefreshTokenRepository _repository = repository;
        private readonly JwtUtils _service = jwt;

        public async Task<RefreshToken?> AuthenticationToken(string _token, CancellationToken ct)
        {
            var token = await _repository.GetToken(_token, ct) ?? throw new Exception("Token not found");
            var valid = _service.ValidadeToken(token.Value);
            return !valid ? throw new NotAuthorizedException("Token expired, please log in again ") : token;
        }
        public async Task<string?> CreateToken(Funcionarios funcionario, CancellationToken ct)
        {
            var token = await _repository.GetTokenByFuncionarioID(funcionario.Id, ct);
            if (token != null)
            {
                var valid = _service.ValidadeToken(token.Value);
                if (valid)
                {
                    return token.Value;
                }
                await _repository.Delete(token, ct);
            }

            token = _service.GenerateToken(funcionario.Id);
            await _repository.Create(token, ct);

            return token.Value;
        }
        public async Task DeleteToken(int Id, CancellationToken ct)
        {
            var token = await _repository.GetTokenByID(Id, ct) ?? throw new NotFoundException("Token not found");
            await _repository.Delete(token, ct);
        }
        public string GetTokenToString(string token)
        {
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                throw new NotFoundException("Token not found");

            return token["Bearer ".Length..].Trim(); ;
        }
    }
}
