using FluentValidation;
using TS.Model.Interfaces;
using TS.Model.Models;

namespace TS.Models.Models.Validations
{
    public class PremioValidation : AbstractValidator<Premio>
    {
        private readonly IPremioService premioService;

        public PremioValidation(IPremioService premioService)
        {
            this.premioService = premioService;

            Validar();
        }

        public void Validar ()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(8).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres")
                .Must(codigo =>
                 {
                     return !premioService.CodigoExistente(codigo);
                 })
                .WithMessage("O este código já existe");

            RuleFor(c => c.DataEnvento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Must(dataEnvento =>
                {
                    return dataEnvento >= DateTime.Now;
                })
                .WithMessage("A data do evento precisa ter uma data futura.");

        }
    }
}