using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Acme.Store.UI.Mvc.Models
{
    public class VendedorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(VendedorModelInfo.NomeMaxLength, MinimumLength = VendedorModelInfo.NomeMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(VendedorModelInfo.EmailMaxLength, MinimumLength = VendedorModelInfo.EmailMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de e-mail válido.")]
        public string Email { get; set; }

    }
}
