using FluentValidation;
using Acme.Store.Auth.Models.Info;

namespace Acme.Store.Auth.Validators
{
    public class EmailValidator : AbstractValidator<EmailValidation>
    {
        public EmailValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .MaximumLength(UsuarioModelInfo.EmailMaxLength).WithMessage("O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres.")
                .EmailAddress().WithMessage("O campo {PropertyName} precisa ser um e-mail válido.");

        }
    }

}
