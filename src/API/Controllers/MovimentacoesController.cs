using ControleFacil.src.Application.DTOs;
using ControleFacil.src.Application.Services;
using ControleFacil.src.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Application.Exceptions;

namespace ControleFacil.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovimentacoesController(MovimentacoesService service) : ControllerBase
    {
        private readonly MovimentacoesService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateMovimentacaoDto dto, CancellationToken ct)
        {
            var response = await _service.AddAsync(dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Movimentacoes>
                {
                    Status = 201,
                    Message = "Sucesso ao criar Movimentacao",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao criar Movimentacao");
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id, CancellationToken ct)
        {
            var response = await _service.GetByIdAsync(Id, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Movimentacoes>
                {
                    Status = 201,
                    Message = "Sucesso ao pegar dados do Movimentacao",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao pegar dados do Movimentacao");
        }
        [HttpGet("item/{Id}")]
        public async Task<IActionResult> GetByItemIdAsync(int Id, CancellationToken ct)
        {
            var response = await _service.GetAllAsync(x => x.ItemId == Id, 0, int.MaxValue, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Movimentacao");
        }
        [HttpGet("funcionario/{Id}")]
        public async Task<IActionResult> GetByFuncionarioIdAsync(int Id, CancellationToken ct)
        {
            var response = await _service.GetAllAsync(x => x.FuncionarioId == Id, 0, int.MaxValue, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Movimentacao");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id, CancellationToken ct)
        {
            await _service.DeleteAsync(Id, ct);

            return Ok(new ApiResponseMessage
            {
                Status = 201,
                Message = "Sucesso ao excluir Movimentacoes",
            });
        }
    }
}