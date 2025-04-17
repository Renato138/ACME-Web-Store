using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Acme.Store.UI.Mvc.Models
{
    public class ProdutoEditarViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(ProdutoModelInfo.NomeMaxLength, MinimumLength = ProdutoModelInfo.NomeMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(ProdutoModelInfo.DescricaoMaxLength, MinimumLength = ProdutoModelInfo.DescricaoMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Descricao { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid? CategoriaId { get; set; }

        [DisplayName("Vendedor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid? VendedorId { get; set; }

        [DisplayName("Preço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        //[Range(0.0, double.MaxValue, ErrorMessage = "O valor do campo {0} não pode ser negativo.")]
        public string Preco { get; set; }

        [DisplayName("Quantidade em Estoque")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        //[Range(0, int.MaxValue, ErrorMessage = "O valor do campo {0} não pode ser negativo.")]
        public string QuantidadeEstoque { get; set; }

        [DisplayName("Unidade de Venda")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public UnidadeVenda UnidadeVenda { get; set; }

        [DisplayName("Imagem do Produto")]
        public IFormFile? ImagemUpload { get; set; }


        public SelectList? CategoriasSelectList { get; set; }

        public SelectList? VendedoresSelectList { get; set; }
    }
}
