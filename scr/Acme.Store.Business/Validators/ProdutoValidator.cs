using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Business.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(ProdutoModelInfo.NomeMinLength, ProdutoModelInfo.NomeMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(ProdutoModelInfo.DescricaoMinLength, ProdutoModelInfo.DescricaoMaxLength).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(p => p.Preco)
                .GreaterThanOrEqualTo(0).WithMessage("O campo {PropertyName} não pode ser negativo.");

            RuleFor(p => p.QuantidadeEstoque)
                .GreaterThanOrEqualTo(0).WithMessage("O campo {PropertyName} não pode ser negativo.");

            RuleFor(p => p.VendedorId)
                .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido.");

            //RuleFor(p => p.Vendedor)
            //    .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(p => p.CategoriaId)
                .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido.");

            //RuleFor(p => p.Categoria)
            //    .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido.");
        }
    }
}
