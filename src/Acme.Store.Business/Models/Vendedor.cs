using Acme.Store.Abstractions.Models;

namespace Acme.Store.Business.Models
{
    public class Vendedor : Entity
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        /* EF Relations */
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
