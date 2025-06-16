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
    public class AmbientesController(AmbientesService service) : ControllerBase
    {
        private readonly AmbientesService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateAmbienteDto dto, CancellationToken ct)
        {
            var response = await _service.AddAsync(dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Ambientes>
                {
                    Status = 201,
                    Message = "Sucesso ao criar Ambiente",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao criar Ambiente");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct, [FromQuery] int offset = 0, [FromQuery] int pageSize = 10)
        {
            var response = await _service.GetAllAsync(x => true, offset, pageSize, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Ambiente");
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id, CancellationToken ct)
        {
            var response = await _service.GetByIdAsync(Id, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Ambientes>
                {
                    Status = 201,
                    Message = "Sucesso ao pegar dados do Ambiente",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao pegar dados do Ambiente");
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id, UpdateAmbienteDto dto, CancellationToken ct)
        {
            var response = await _service.UpdateAsync(Id, dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Ambientes>
                {
                    Status = 201,
                    Message = "Sucesso ao atualizar dados do Ambiente",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao atualizar dados do Ambiente");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id, CancellationToken ct)
        {
            await _service.DeleteAsync(Id, ct);

            return Ok(new ApiResponseMessage
            {
                Status = 201,
                Message = "Sucesso ao excluir Ambientes",
            });
        }
    }
}