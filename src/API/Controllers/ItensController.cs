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
    public class ItensController(ItensService service) : ControllerBase
    {
        private readonly ItensService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateItemDto dto, CancellationToken ct)
        {
            var response = await _service.AddAsync(dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Itens>
                {
                    Status = 201,
                    Message = "Sucesso ao criar Item",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao criar Item");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct, [FromQuery] int offset = 0, [FromQuery] int pageSize = int.MaxValue)
        {
            var response = await _service.GetAllAsync(x => true, offset, pageSize, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Item");
        }
        [HttpGet("Armario/{ArmarioId}")]
        public async Task<IActionResult> GetAllByArmarioIdAsync(int ArmarioId, CancellationToken ct, [FromQuery] int offset = 0, [FromQuery] int pageSize = int.MaxValue)
        {
            var response = await _service.GetAllAsync(x => x.ArmarioId == ArmarioId, offset, pageSize, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Item");
        }
        [HttpGet("Armario/{ArmarioId}/{TipoPatrimonio}")]
        public async Task<IActionResult> GetAllByArmarioIdAsync(int ArmarioId, int TipoPatrimonio, CancellationToken ct, [FromQuery] int offset = 0, [FromQuery] int pageSize = int.MaxValue)
        {
            var response = await _service.GetAllAsync(x => x.ArmarioId == ArmarioId && ((int)x.TipoPatrimonio) == TipoPatrimonio, offset, pageSize, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Item");
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id, CancellationToken ct)
        {
            var response = await _service.GetByIdAsync(Id, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Itens>
                {
                    Status = 201,
                    Message = "Sucesso ao pegar dados do Item",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao pegar dados do Item");
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id, UpdateItemDto dto, CancellationToken ct)
        {
            var response = await _service.UpdateAsync(Id, dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Itens>
                {
                    Status = 201,
                    Message = "Sucesso ao atualizar dados do Item",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao atualizar dados do Item");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id, CancellationToken ct)
        {
            await _service.DeleteAsync(Id, ct);

            return Ok(new ApiResponseMessage
            {
                Status = 201,
                Message = "Sucesso ao excluir Itens",
            });
        }
    }
}