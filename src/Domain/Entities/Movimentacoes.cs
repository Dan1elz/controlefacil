using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities.Base;
using ControleFacil.src.Domain.Enums;

namespace ControleFacil.src.Domain.Entities
{
    public class Movimentacoes : BaseEntity
    {
        [ForeignKey("Itens"), Required(ErrorMessage = "O Id do Item é obrigatório.")]
        public int ItemId {get; private set;}
        public virtual Itens? Item {get; set;}
        
        [ForeignKey("Funcionario"), Required(ErrorMessage = "O Id do Funcionario é obrigatório.")]
        public int FuncionarioId {get; private set;}
        public virtual Funcionarios? Funcionario {get; set;}

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public TipoMovimentacao TipoMovimentacao {get; private set;}
        public Movimentacoes() : base() {}
        public Movimentacoes(CreateMovimentacaoDto dto) : base()
        {
            FuncionarioId = dto.FuncionarioId;
            ItemId = dto.ItemId;
            TipoMovimentacao = dto.TipoMovimentacao;
        }
    }
}