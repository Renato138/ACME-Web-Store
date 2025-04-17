using Acme.Store.Business.Models.Info;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Acme.Store.UI.Mvc.Models
{
    public class CategoriaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(CategoriaModelInfo.NomeMaxLength, MinimumLength = CategoriaModelInfo.NomeMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(CategoriaModelInfo.DescricaoMaxLength, MinimumLength = CategoriaModelInfo.DescricaoMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Descricao { get; set; }

    }
}
