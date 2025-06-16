using System.ComponentModel.DataAnnotations;
using ControleFacil.src.Domain.DTOs;
using ControleFacil.src.Domain.Entities.Base;

namespace ControleFacil.src.Domain.Entities
{
    public class Funcionarios : BaseEntity
    {
        [Required(ErrorMessage = "O Nif é obrigatório.")]
        public int Nif {get; private set;}

        [Required(ErrorMessage = "O nome é obrigatório."), StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
        public string Nome {get; private set;} = string.Empty;
        
        [Required(ErrorMessage = "O departamento é obrigatório."), StringLength(50, ErrorMessage = "O departamento deve ter no máximo 50 caracteres.")]
        public string Departamento {get; private set;} = string.Empty; 
        public string Senha {get; private set;} = string.Empty;
        public virtual ICollection<Movimentacoes> Movimentacoes {get; set;} = [];
        
        public Funcionarios() : base() {}
        public Funcionarios(CreateFuncionarioDto dto) : base()
        {
            Nif = dto.Nif;
            Nome = dto.Nome;
            Departamento = dto.Departamento;
            Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        }
        public void Update(UpdateFuncionarioDto dto)
        {
            Nif = dto.Nif;
            Nome = dto.Nome;
            Departamento = dto.Departamento;
            base.Update();
        } 
        public void UpdateSenha(string senha) 
        {
            Senha = senha;
        }
    }
}