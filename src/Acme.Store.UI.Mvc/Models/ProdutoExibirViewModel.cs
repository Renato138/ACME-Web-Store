using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Acme.Store.UI.Mvc.Models
{
    public class ProdutoExibirViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public string Categoria { get; set; }

        public string Vendedor { get; set; }

        public Guid VendedorId { get; set; }

        [DisplayName("Preço")]
        public string Preco { get; set; }

        [DisplayName("Quantidade em Estoque")]
        public string QuantidadeEstoque { get; set; }

        [DisplayName("Unidade de Venda")]
        public string UnidadeVenda { get; set; }

        [DisplayName("Imagem do Produto")]
        public string Imagem { get; set; }
    }
}
