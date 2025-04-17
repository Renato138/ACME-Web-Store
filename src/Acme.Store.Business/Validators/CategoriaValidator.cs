using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;
using FluentValidation;

namespace Acme.Store.Business.Validators
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(CategoriaModelInfo.NomeMinLength, CategoriaModelInfo.NomeMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(CategoriaModelInfo.DescricaoMinLength, CategoriaModelInfo.DescricaoMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");


        }
    }
}
