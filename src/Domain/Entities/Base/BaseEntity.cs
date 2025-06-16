using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFacil.src.Domain.Entities.Base 
{
    public abstract class BaseEntity
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; private set;}
        public DateTime CreateAt {get; private init;}
        public DateTime UpdateAt {get; private set;}

        protected BaseEntity() {
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        protected void Update() {
            UpdateAt = DateTime.UtcNow;
        }
        public void Validate()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                var errors = validationResults
                    .Where(vr => !string.IsNullOrWhiteSpace(vr.ErrorMessage))
                    .Select(vr => vr.ErrorMessage!)
                    .ToList();

                throw new Application.Exceptions.ValidationException(errors);
            }
        }

    }
}