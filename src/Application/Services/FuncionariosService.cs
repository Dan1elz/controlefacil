
#pragma warning disable CS9107

using ControleFacil.src.Application.Exceptions;
using ControleFacil.src.Application.Services.Base;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Application.DTOs;
using ControleFacil.src.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.src.Application.Services
{
    public class FuncionariosService(IFuncionariosRepository repository, RefreshTokenService service, ApplicationDbContext context) : BaseService<Funcionarios, IFuncionariosRepository>(repository, context)
    {
        private readonly RefreshTokenService _service = service;
        public async Task<LoginResponseDto> Login(LoginFuncionarioDto dto, CancellationToken ct)
        {
            var response = await _repository.Login(dto.Nif, ct) ?? throw new BadRequestException("Nif inválido.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Senha, response.Senha))
                throw new BadRequestException("Senha inválida.");

            var token = await _service.CreateToken(response, ct) ?? throw new BadRequestException("Erro ao criar token. Contacte o suporte.");

            return new LoginResponseDto
            {
                FuncionarioId = response.Id,
                Token = token
            };
        }
        public async Task<Funcionarios> UpdateAsync(int Id, UpdateFuncionarioDto dto, CancellationToken ct)
        {
            var response = await _repository.GetByIdAsync(Id, ct) ?? throw new NotFoundException("Funcionario não encontrado");

            response.Update(dto);
            await _repository.Update(response, ct);
            return response;
        }
        public async Task<Funcionarios> UpdatePassword(int Id, UpdateFuncionarioSenhaDto dto, CancellationToken ct)
        {
            var response = await _repository.GetAsync(u => u.Id == Id, ct) ?? throw new NotFoundException("Usuario não encontrado");

            response.UpdateSenha(BCrypt.Net.BCrypt.HashPassword(dto.Senha));
            response.Validate();
            await _repository.Update(response, ct);
            return response;
        }
        public async Task<Funcionarios> AddAsync(CreateFuncionarioDto dto, CancellationToken ct)
        {
            var response = new Funcionarios(dto);
            response.Validate();
            await _repository.AddAsync(response, ct);
            return response;
        }
        public async override Task DeleteAsync(int Id, CancellationToken ct)
        {
            Funcionarios response = await repository.GetByIdAsync(Id, ct, includeExpression: x => x.Include(u => u.Movimentacoes)) ?? throw new NotFoundException("Funcionario não encontrado");
            if (response.Movimentacoes.Any())
            {
                throw new BadRequestException("Não é possível excluir o Funcionario, pois existem Movimentacoes associados a ele.");
            }
            await _repository.Remove(response, ct);
        }
    }
}