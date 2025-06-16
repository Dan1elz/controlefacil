using ControleFacil.src.Domain.Enums;

namespace ControleFacil.src.Domain.DTOs;
public class CreateMovimentacaoDto
{
    public int FuncionarioId {get; set;}
    public int ItemId {get; set;}
    public TipoMovimentacao TipoMovimentacao {get; set;}
}
