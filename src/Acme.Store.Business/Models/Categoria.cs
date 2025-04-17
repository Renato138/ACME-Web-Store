using Acme.Store.Abstractions.Models;

namespace Acme.Store.Business.Models
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        /* EF Relations */
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
