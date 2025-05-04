using Acme.Store.Auth.Models.Info;
using Acme.Store.Business.Models.Info;
using System.ComponentModel.DataAnnotations;

namespace Acme.Store.UI.Mvc.Models
{
    public class VendedorIncluirViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(VendedorModelInfo.NomeMaxLength, MinimumLength = VendedorModelInfo.NomeMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(VendedorModelInfo.EmailMaxLength, MinimumLength = VendedorModelInfo.EmailMinLength, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de e-mail válido.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "A Senha é obrigatória")]
        [StringLength(UsuarioModelInfo.SenhaMaxLength, ErrorMessage = "A Senha precisa ter entre {2} e {1} caracteres", MinimumLength = UsuarioModelInfo.SenhaMinLength)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirme a senha")]
        [StringLength(UsuarioModelInfo.SenhaMaxLength, ErrorMessage = "A Senha precisa ter entre {2} e {1} caracteres", MinimumLength = UsuarioModelInfo.SenhaMinLength)]
        [Required(ErrorMessage = "O campo Confirmação da Senha é obrigatório.")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        [DataType(DataType.Password)]
        public string ConfirmeSenha { get; set; }
    }
}
