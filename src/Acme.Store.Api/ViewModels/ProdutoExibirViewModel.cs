using System.ComponentModel;
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

        public Guid CategoriaId { get; set; }

        public string Categoria { get; set; }

        public Guid VendedorId { get; set; }

        public string Vendedor { get; set; }

        public decimal Preco { get; set; }

        public int QuantidadeEstoque { get; set; }

        public string UnidadeVenda { get; set; }

        public string Imagem { get; set; }
    }

}
