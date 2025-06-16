using System.ComponentModel.DataAnnotations;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities.Base;

namespace ControleFacil.src.Domain.Entities
{
    public class Ambientes : BaseEntity
    {
        [Required(ErrorMessage = "O código é obrigatório."), StringLength(32, ErrorMessage = "O código deve ter no máximo 32 caracteres.")]
        public string Codigo {get; private set;} = string.Empty;
        
        [Required(ErrorMessage = "O nome é obrigatório."), StringLength(50, ErrorMessage ="O nome deve ter no máximo 50 caracteres.") ]
        public string Nome {get; private set;} = string.Empty;
        
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Localizacao {get; private set;} = string. Empty;
        public virtual ICollection<Armarios> Armarios {get; set;} = [];

        public Ambientes() : base() {}
        public Ambientes(CreateAmbienteDto dto)  : base()
        {
            Codigo = dto.Codigo;
            Nome = dto.Nome;
            Localizacao = dto.Localizacao;
        }
        public void Update(UpdateAmbienteDto dto)
        {
            Codigo = dto.Codigo;
            Nome = dto.Nome;
            Localizacao = dto.Localizacao;
            base.Update();
        }
    }
}