using ControleFacil.src.Domain.Enums;

namespace ControleFacil.src.Domain.DTOs;

public class CreateItemDto
{
    public int ArmarioId {get; set;}
    public int Ni {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Descricao {get; set;} = string.Empty;
    public TipoPatrimonio TipoPatrimonio {get; set;}
}
public class UpdateItemDto : CreateItemDto 
{
    public bool Disponivel {get; set;}
}