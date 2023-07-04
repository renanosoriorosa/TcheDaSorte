using FluentValidation;
using TS.Model.Models;

namespace TS.Models.Models.Validations
{
    public class CartelaValidation : AbstractValidator<Cartela>
    {
        public CartelaValidation()
        {
            RuleFor(c => c.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8).WithMessage("O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres");

            RuleFor(c => c.PremioId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o Premio");

            RuleFor(c => c.Preco)
                .NotEmpty().WithMessage("O campo Preço precisa ser fornecido")
                .GreaterThan(0).WithMessage("O campo Preço precisa ser maior que zero");

            RuleFor(c => c.PrimeiroNumero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que zero");

            RuleFor(c => c.SegundoNumero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que zero");

            RuleFor(c => c.TerceiroNumero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que zero");

            RuleFor(c => c.QuartoNumero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que zero");

            RuleFor(c => c.QuintoNumero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que zero");
        }
    }
}