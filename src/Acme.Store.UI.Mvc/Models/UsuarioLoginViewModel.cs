using Acme.Store.Auth.Models.Info;
using Acme.Store.Business.Models.Info;
using System.ComponentModel.DataAnnotations;

namespace Acme.Store.UI.Mvc.Models
{
    public class UsuarioLoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(VendedorModelInfo.EmailMaxLength, MinimumLength = VendedorModelInfo.EmailMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de e-mail válido.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(UsuarioModelInfo.SenhaMaxLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = UsuarioModelInfo.SenhaMinLength)]
        public string Senha { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }
    }
}
