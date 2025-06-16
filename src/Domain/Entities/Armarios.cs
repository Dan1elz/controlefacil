using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities.Base;
using ControleFacil.src.Domain.Enums;

namespace ControleFacil.src.Domain.Entities
{
    public class Armarios : BaseEntity
    {
        [ForeignKey("Ambientes"), Required(ErrorMessage = "O Id do Ambiente é obrigatório.")]
        public int AmbienteId {get; private set;}
        public virtual Ambientes? Ambiente {get; set;}

        [Required(ErrorMessage = "O Ni é obrigatório.")]
        public int Ni {get; private set;}

        [Required(ErrorMessage = "O nome é obrigatório."), StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
        public string Nome {get; private set;} = string.Empty;
        
        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public TipoArmario TipoArmario {get; private set;}
        public virtual ICollection<Itens> Itens {get; set;} = [];
        
        public Armarios() : base() {}
        public Armarios(CreateArmarioDto dto) : base()
        {
            AmbienteId = dto.AmbienteId;
            Ni = dto.Ni;
            Nome = dto.Nome;
            TipoArmario = dto.TipoArmario;
        }
        public void Update(UpdateArmarioDto dto)
        {
            AmbienteId = dto.AmbienteId;
            Ni = dto.Ni;
            Nome = dto.Nome;
            TipoArmario = dto.TipoArmario;
            base.Update();
        } 
    }
}