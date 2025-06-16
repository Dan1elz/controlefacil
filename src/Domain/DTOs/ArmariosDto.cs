using ControleFacil.src.Domain.Enums;

namespace ControleFacil.src.Domain.DTOs;

public class CreateArmarioDto
{
    public int AmbienteId {get; set;}
    public int Ni {get; set;}
    public string Nome {get; set;} = string.Empty;
    public TipoArmario TipoArmario {get; set;}
}
public class UpdateArmarioDto : CreateArmarioDto {}
