using FluentValidation;
using Acme.Store.Auth.Models.Info;

namespace Acme.Store.Auth.Validators
{
    public class SenhaValidator : AbstractValidator<SenhaValidation>
    {
        public SenhaValidator()
        {
            RuleFor(v => v.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(UsuarioModelInfo.SenhaMinLength, UsuarioModelInfo.SenhaMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(v => v.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa é obrigatório.")
                .Length(UsuarioModelInfo.SenhaMinLength, UsuarioModelInfo.SenhaMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.")
                .Matches(@"[A-Z]+").WithMessage("O campo {PropertyName} precisa conter ao menos uma letra maiúscula.")
                .Matches(@"[a-z]+").WithMessage("O campo {PropertyName} precisa conter ao menos uma letra minúscula.")
                .Matches(@"[0-9]+").WithMessage("O campo {PropertyName} precisa conter ao menos um número.")
                .Matches(@"[\!\?\*\.]+").WithMessage("O campo {PropertyName} precisa conter ao menos um destes caracteres: '.', '!', '?', '*'");

        }
    }

}
