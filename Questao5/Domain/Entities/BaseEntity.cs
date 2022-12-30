using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    public abstract class BaseEntity<T>
    {
        [NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public string Id { get; set; }
        public virtual bool IsValid()
        {
            return true;
        }
    }
}
