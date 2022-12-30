using FluentValidation;
using Questao5.Domain.Resources;

namespace Questao5.Domain.Entities
{
    public class ContaCorrente : BaseEntity<ContaCorrente>
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public int Ativo { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new ContaCorrenteValidator().Validate(this);

            return ValidationResult.IsValid;
        }
    }
    public class ContaCorrenteValidator : AbstractValidator<ContaCorrente>
    {
        public ContaCorrenteValidator()
        {
            RuleFor(x => x.Ativo)
                .Equal(1)
                .WithMessage(MessagesResource.INACTIVE_ACCOUNT);

        }
    }
}
