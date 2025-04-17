using Acme.Store.Business.Models.Info;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Acme.Store.Api.ViewModels
{
    public class CategoriaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(CategoriaModelInfo.NomeMaxLength, MinimumLength = CategoriaModelInfo.NomeMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(CategoriaModelInfo.DescricaoMaxLength, MinimumLength = CategoriaModelInfo.DescricaoMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Descricao { get; set; }

    }
}
