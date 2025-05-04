using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Store.Auth.Models;
using Acme.Store.Auth.Models.Info;

namespace Acme.Store.Auth.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            //RuleFor(v => v.Nome)
            //   .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
            //   .Length(UsuarioModelInfo.NomeMinLength, UsuarioModelInfo.NomeMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(v => v.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(UsuarioModelInfo.SenhaMinLength, UsuarioModelInfo.SenhaMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(p => p.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa é obrigatório.")
                .Length(UsuarioModelInfo.SenhaMinLength, UsuarioModelInfo.SenhaMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.")
                .Matches(@"[A-Z]+").WithMessage("O campo {PropertyName} precisa conter ao menos uma letra maiúscula.")
                .Matches(@"[a-z]+").WithMessage("O campo {PropertyName} precisa conter ao menos uma letra minúscula.")
                .Matches(@"[0-9]+").WithMessage("O campo {PropertyName} precisa conter ao menos um número.")
                .Matches(@"[\!\?\*\.]+").WithMessage("O campo {PropertyName} precisa conter ao menos um destes caracteres: '.', '!', '?', '*'");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .MaximumLength(UsuarioModelInfo.EmailMaxLength).WithMessage("O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres.")
                .EmailAddress().WithMessage("O campo {PropertyName} precisa ser um e-mail válido.");

        }
    }

}
