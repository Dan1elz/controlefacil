
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
    public class AmbientesService(IAmbientesRepository repository, ApplicationDbContext context) : BaseService<Ambientes, IAmbientesRepository>(repository, context)
    {
        public async Task<Ambientes> UpdateAsync(int Id, UpdateAmbienteDto dto, CancellationToken ct)
        {
            var response = await _repository.GetByIdAsync(Id, ct) ?? throw new NotFoundException("Ambiente não encontrado");

            response.Update(dto);
            await _repository.Update(response, ct);
            return response;
        }
        public async Task<Ambientes> AddAsync(CreateAmbienteDto dto, CancellationToken ct)
        {
            var response = new Ambientes(dto);
            response.Validate();
            await _repository.AddAsync(response, ct);
            return response;
        }
        public async override Task DeleteAsync(int Id, CancellationToken ct)
        {
            Ambientes response = await repository.GetByIdAsync(Id, ct, includeExpression: x => x.Include(u => u.Armarios)) ?? throw new NotFoundException("Ambiente não encontrado");
            if (response.Armarios.Any())
            {
                throw new BadRequestException("Não é possível excluir o ambiente, pois existem armarios associados a ele.");
            }
            await _repository.Remove(response, ct);
        }
    }
}