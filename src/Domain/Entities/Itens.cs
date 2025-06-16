using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities.Base;
using ControleFacil.src.Domain.Enums;

namespace ControleFacil.src.Domain.Entities
{
    public class Itens : BaseEntity
    {
        [ForeignKey("Armarios"), Required(ErrorMessage = "O Id do Armario é obrigatório.")]
        public int ArmarioId {get; private set;}
        public virtual Armarios? Armario {get; set;}

        [Required(ErrorMessage = "O Ni é obrigatório.")]
        public int Ni {get; private set;}

        [Required(ErrorMessage = "O nome é obrigatório."), StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
        public string Nome {get; private set;} = string.Empty;
        
        [Required(ErrorMessage = "A descrição é obrigatória."), StringLength(2000, ErrorMessage = "A descrição deve ter no máximo 2000 caracteres.")]
        public string Descricao {get; private set;} = string. Empty;

        [Required(ErrorMessage = "O status é obrigatório.")]
        public bool Disponivel {get; private set;}

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public TipoPatrimonio TipoPatrimonio {get; private set;}
        public virtual ICollection<Movimentacoes> Movimentacoes {get; set;} = [];
        
        public Itens() : base() {}
        public Itens(CreateItemDto dto) : base()
        {
            ArmarioId = dto.ArmarioId;
            Ni = dto.Ni;
            Nome = dto.Nome;
            Descricao = dto.Descricao;
            Disponivel = true;
            TipoPatrimonio = dto.TipoPatrimonio;
        }
        public void Update(UpdateItemDto dto)
        {
            ArmarioId = dto.ArmarioId;
            Ni = dto.Ni;
            Nome = dto.Nome;
            Descricao = dto.Descricao;
            Disponivel = dto.Disponivel;
            TipoPatrimonio = dto.TipoPatrimonio;
            base.Update();
        }
        public void SetDisponivel(bool disponivel)
        {
            Disponivel = disponivel;
            base.Update();
        }
    }
}