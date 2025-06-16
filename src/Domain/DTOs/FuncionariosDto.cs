namespace ControleFacil.src.Domain.DTOs;

public class CreateFuncionarioDto
{
    public int Nif {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Departamento {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty;
   
}
public class UpdateFuncionarioDto : CreateFuncionarioDto {}