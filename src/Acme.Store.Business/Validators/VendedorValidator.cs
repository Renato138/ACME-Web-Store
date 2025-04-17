using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;
using FluentValidation;

namespace Acme.Store.Business.Validators
{
    public class VendedorValidator : AbstractValidator<Vendedor>
    {
        public VendedorValidator()
        {
            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(VendedorModelInfo.NomeMinLength, VendedorModelInfo.NomeMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .MaximumLength(VendedorModelInfo.EmailMaxLength).WithMessage("O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres.")
                .EmailAddress().WithMessage("O campo {PropertyName} precisa ser um e-mail válido.");

        }
    }
}
