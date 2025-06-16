namespace ControleFacil.src.Domain.DTOs;
public class CreateAmbienteDto
{
    public string Codigo {get; set;} = string.Empty;
    public string Nome {get; set;} = string.Empty;
    public string Localizacao {get; set;} = string.Empty;
}
public class UpdateAmbienteDto : CreateAmbienteDto {}
