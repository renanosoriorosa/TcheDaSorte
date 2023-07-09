using FluentValidation;
using TS.Model.Models;

namespace TS.Models.Models.Validations
{
    public class PremioValidation : AbstractValidator<Premio>
    {
        public PremioValidation()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.DataEnvento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MustAsync(async (dataEnvento, context) =>
                {
                    return dataEnvento >= DateTime.Now;
                })
                .WithMessage("A data do evento precisa ter uma data futura.");

        }
    }
}