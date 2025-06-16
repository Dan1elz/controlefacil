
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
    public class ArmariosService(IArmariosRepository repository, ApplicationDbContext context) : BaseService<Armarios, IArmariosRepository>(repository, context)
    {
        public async Task<Armarios> UpdateAsync(int Id, UpdateArmarioDto dto, CancellationToken ct)
        {
            var response = await _repository.GetByIdAsync(Id, ct) ?? throw new NotFoundException("Armario não encontrado");

            response.Update(dto);
            await _repository.Update(response, ct);
            return response;
        }
        public async Task<Armarios> AddAsync(CreateArmarioDto dto, CancellationToken ct)
        {
            var response = new Armarios(dto);
            response.Validate();
            await _repository.AddAsync(response, ct);
            return response;
        }
        public async override Task DeleteAsync(int Id, CancellationToken ct)
        {
            Armarios response = await repository.GetByIdAsync(Id, ct, includeExpression: x => x.Include(u => u.Itens)) ?? throw new NotFoundException("Armario não encontrado");
            if (response.Itens.Any())
            {
                throw new BadRequestException("Não é possível excluir o Armario, pois existem Itens associados a ele.");
            }
            await _repository.Remove(response, ct);
        }
    }
}