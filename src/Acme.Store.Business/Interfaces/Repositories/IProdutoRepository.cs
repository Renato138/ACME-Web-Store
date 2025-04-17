using Acme.Store.Abstractions.Interfaces.Repositories;
using Acme.Store.Business.Models;

namespace Acme.Store.Business.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterPorCategoria(Guid categoriaId);

        Task<IEnumerable<Produto>> ObterPorVendedor(Guid vendedorId);

        Task<IEnumerable<Produto>> ObterPorCategoriaVendedor(Guid categoriaId, Guid vendedorId);

        Task<Produto> ObterPorNome(string nome);

        Task<bool> ExisteNome(string nome);

    }
}
