using System.ComponentModel.DataAnnotations;
using Acme.Store.Business.Models;

namespace Acme.Store.Api.ViewModels
{
    public class ProdutoExibirViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public Categoria Categoria { get; set; }

        public Vendedor Vendedor { get; set; }

        public decimal Preco { get; set; }

        public int QuantidadeEstoque { get; set; }

        public UnidadeVenda UnidadeVenda { get; set; }

        public string ImagemBase64 { get; set; }
    }

}
