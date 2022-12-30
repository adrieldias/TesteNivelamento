using FluentValidation;
using Questao5.Domain.Resources;

namespace Questao5.Domain.Entities
{
    public class Movimento : BaseEntity<Movimento>
    {
        public string IdContaCorrente { get; set; }
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public double Valor { get; set; }
        public override bool IsValid()
        {

            ValidationResult = new MovimentoValidator().Validate(this);

            return ValidationResult.IsValid;
        }
    }
    public class MovimentoValidator : AbstractValidator<Movimento>
    {
        public MovimentoValidator()
        {
            RuleFor(c => c.Valor)
                .GreaterThan(0)
                .WithMessage(MessagesResource.INVALID_VALUE);
            RuleFor(c => c.TipoMovimento)
                .Must(x => new[] { "C", "D" }.Contains(x))
                .WithMessage(MessagesResource.INVALID_TYPE);

        }
    }
}
