using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleFacil.src.Domain.Entities.Base;

namespace ControleFacil.src.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        [ForeignKey("Funcionario"), Required(ErrorMessage = "O Id do Funcionario é obrigatório")]
        public int FuncionarioId {get; private set;}
        public virtual Funcionarios? Funcionario {get; set;}
        
        [Required(ErrorMessage = "Por favor insira o Token")]
        public string Value {get; set;} = string.Empty;
        private RefreshToken() { }
        public RefreshToken(int funcionarioId, string value)
        {
            FuncionarioId = funcionarioId;
            Value = value;
        }
    }
}