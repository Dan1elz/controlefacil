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
    public class ArmariosController(ArmariosService service) : ControllerBase
    {
        private readonly ArmariosService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateArmarioDto dto, CancellationToken ct)
        {
            var response = await _service.AddAsync(dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Armarios>
                {
                    Status = 201,
                    Message = "Sucesso ao criar Armario",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao criar Armario");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct, [FromQuery] int offset = 0, [FromQuery] int pageSize = int.MaxValue)
        {
            var response = await _service.GetAllAsync(x => true, offset, pageSize, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Armario");
        }
        [HttpGet("Ambiente/{AmbienteId}")]
        public async Task<IActionResult> GetAllByAmbienteIdAsync(int AmbienteId, CancellationToken ct, [FromQuery] int offset = 0, [FromQuery] int pageSize = int.MaxValue)
        {
            var response = await _service.GetAllAsync(x => x.AmbienteId == AmbienteId, offset, pageSize, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Armario");
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id, CancellationToken ct)
        {
            var response = await _service.GetByIdAsync(Id, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Armarios>
                {
                    Status = 201,
                    Message = "Sucesso ao pegar dados do Armario",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao pegar dados do Armario");
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id, UpdateArmarioDto dto, CancellationToken ct)
        {
            var response = await _service.UpdateAsync(Id, dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Armarios>
                {
                    Status = 201,
                    Message = "Sucesso ao atualizar dados do Armario",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao atualizar dados do Armario");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id, CancellationToken ct)
        {
            await _service.DeleteAsync(Id, ct);

            return Ok(new ApiResponseMessage
            {
                Status = 201,
                Message = "Sucesso ao excluir Armarios",
            });
        }
    }
}