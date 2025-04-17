using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Acme.Store.Api.ViewModels
{
    public class ProdutoEditarViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(ProdutoModelInfo.NomeMaxLength, MinimumLength = ProdutoModelInfo.NomeMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(ProdutoModelInfo.DescricaoMaxLength, MinimumLength = ProdutoModelInfo.DescricaoMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid? CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid? VendedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "O valor do campo {0} não pode ser negativo.")]
        public double Preco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O valor do campo {0} não pode ser negativo.")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public UnidadeVenda UnidadeVenda { get; set; }

        public string? ImagemBase64 { get; set; }

    }

}
