namespace ControleFacil.src.Application.DTOs;

public class LoginResponseDto 
{
    public int FuncionarioId { get; set; }
    public string Token { get; set; } = string.Empty;
}

public class UpdateFuncionarioSenhaDto
{
    public required string Senha { get; set; }
}

public class LoginFuncionarioDto
{
    public int Nif { get; set; }
    public string Senha { get; set; } = string.Empty;
}
