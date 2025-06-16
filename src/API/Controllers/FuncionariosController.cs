using ControleFacil.src.Application.DTOs;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities;
using ControleFacil.src.Application.Services;
using ControleFacil.src.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ControleFacil.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController(FuncionariosService service, RefreshTokenService token) : ControllerBase
    {
        private readonly RefreshTokenService _token = token;
        private readonly FuncionariosService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateFuncionarioDto dto, CancellationToken ct)
        {
            var response = await _service.AddAsync(dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Funcionarios>
                {
                    Status = 201,
                    Message = "Sucesso ao criar Funcionario",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao criar Funcionario");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginFuncionario(LoginFuncionarioDto dto, CancellationToken ct)
        {
            var response = await _service.Login(dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<LoginResponseDto>
                {
                    Status = 201,
                    Message = "Sucesso ao logar funcionario",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao logar funcionario");
        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutFuncionario(CancellationToken ct)
        {
            var token = await _token.AuthenticationToken(_token.GetTokenToString(HttpContext.Request.Headers.Authorization.ToString()), ct);
            await _token.DeleteToken(token!.Id, ct);
           
            return Ok(new ApiResponseMessage
            {
                Status = 201,
                Message = "Sucesso ao deslogar funcionario",
            });
        }
        [Authorize]
        [HttpPatch("NewPassword")]
        public async Task<IActionResult> UpdatePassword(UpdateFuncionarioSenhaDto dto, CancellationToken ct)
        {
            var token = await _token.AuthenticationToken(_token.GetTokenToString(HttpContext.Request.Headers.Authorization.ToString()), ct);

            var response = await _service.UpdatePassword(token!.FuncionarioId, dto, ct);
            await _token.DeleteToken(token.Id, ct);
            var newToken = await _token.CreateToken(response, ct);

            return Ok(new ApiResponse<string>
            {
                Status = 201,
                Message = "Sucesso ao atualizar a senha do funcionario",
                Data = newToken!
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct, [FromQuery] int offset = 0, [FromQuery] int pageSize = 10)
        {
            var response = await _service.GetAllAsync(x => true, offset, pageSize, ct);
            if (response != null)
                return Ok(response);
            
            throw new BadRequestException("Erro ao pegar dados do Funcionario");
        }
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int Id, CancellationToken ct)
        {
            var response = await _service.GetByIdAsync(Id, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Funcionarios>
                {
                    Status = 201,
                    Message = "Sucesso ao pegar dados do Funcionario",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao pegar dados do Funcionario");
        }
        [HttpPut("{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(int Id, UpdateFuncionarioDto dto, CancellationToken ct)
        {
            var response = await _service.UpdateAsync(Id, dto, ct);
            if (response != null)
            {
                return Ok(new ApiResponse<Funcionarios>
                {
                    Status = 201,
                    Message = "Sucesso ao atualizar dados do Funcionario",
                    Data = response
                });
            }

            throw new BadRequestException("Erro ao atualizar dados do Funcionario");
        }
        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int Id, CancellationToken ct)
        {
            await _service.DeleteAsync(Id, ct);

            return Ok(new ApiResponseMessage
            {
                Status = 201,
                Message = "Sucesso ao excluir Funcionarios",
            });
        }
    }
}