
#pragma warning disable CS9107

using ControleFacil.src.Application.Exceptions;
using ControleFacil.src.Application.Services.Base;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Domain.Enums;
using ControleFacil.src.Domain.Interfaces;
using ControleFacil.src.Infrastructure;

namespace ControleFacil.src.Application.Services
{
    public class MovimentacoesService(IMovimentacoesRepository repository, IItensRepository itensRepository, ApplicationDbContext context) : BaseService<Movimentacoes, IMovimentacoesRepository>(repository, context)
    {
        private readonly IItensRepository _itensRepository = itensRepository;
        public async Task<Movimentacoes> AddAsync(CreateMovimentacaoDto dto, CancellationToken ct)
        {
            var item = await _itensRepository.GetByIdAsync(dto.ItemId, ct) ?? throw new NotFoundException("Item não encontrado");

            if(dto.TipoMovimentacao == TipoMovimentacao.Retirada) {
                if(!item.Disponivel) throw new BadRequestException("O item já está retirado.");
                item.SetDisponivel(false);
                await _itensRepository.Update(item, ct);
            } else if(dto.TipoMovimentacao == TipoMovimentacao.Devolucao) {
                if(item.Disponivel) throw new BadRequestException("O item já está Disponivel.");
                item.SetDisponivel(true);
                await _itensRepository.Update(item, ct);
            } else {
                throw new BadRequestException("Tipo de movimentação inválido");
            }

            var response = new Movimentacoes(dto);
            response.Validate();
            await _repository.AddAsync(response, ct);
            return response;
        }
        public async override Task DeleteAsync(int Id, CancellationToken ct)
        {
            Movimentacoes response = await repository.GetByIdAsync(Id, ct) ?? throw new NotFoundException("Movimentacao não encontrado");
            await _repository.Remove(response, ct);
        }
    }
}