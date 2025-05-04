using Acme.Store.Auth.Models.Info;
using System.ComponentModel.DataAnnotations;

namespace Acme.Store.Api.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(UsuarioModelInfo.NomeMaxLength, MinimumLength = UsuarioModelInfo.NomeMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(UsuarioModelInfo.EmailMaxLength, MinimumLength = UsuarioModelInfo.EmailMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(UsuarioModelInfo.SenhaMaxLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = UsuarioModelInfo.SenhaMinLength)]
        public string Senha { get; set; }
    }
}
