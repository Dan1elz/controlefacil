
#pragma warning disable CS9107

using ControleFacil.src.Application.Exceptions;
using ControleFacil.src.Application.Services.Base;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.src.Application.Services
{
    public class ItensService(IItensRepository repository, ApplicationDbContext context) : BaseService<Itens, IItensRepository>(repository, context)
    {
        public async Task<Itens> UpdateAsync(int Id, UpdateItemDto dto, CancellationToken ct)
        {
            var response = await _repository.GetByIdAsync(Id, ct) ?? throw new NotFoundException("Item não encontrado");

            response.Update(dto);
            await _repository.Update(response, ct);
            return response;
        }
        public async Task<Itens> AddAsync(CreateItemDto dto, CancellationToken ct)
        {
            var response = new Itens(dto);
            response.Validate();
            await _repository.AddAsync(response, ct);
            return response;
        }
        public async override Task DeleteAsync(int Id, CancellationToken ct)
        {
            Itens response = await repository.GetByIdAsync(Id, ct, includeExpression: x => x.Include(u => u.Movimentacoes)) ?? throw new NotFoundException("Item não encontrado");
            if (response.Movimentacoes.Any())
            {
                throw new BadRequestException("Não é possível excluir o Item, pois existem Movimentacoes associados a ele.");
            }
            await _repository.Remove(response, ct);
        }
    }
}