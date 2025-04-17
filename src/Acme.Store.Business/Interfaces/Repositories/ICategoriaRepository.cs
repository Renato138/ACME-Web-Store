using Acme.Store.Abstractions.Interfaces.Repositories;
using Acme.Store.Business.Models;

namespace Acme.Store.Business.Interfaces.Repositories
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> ObterPorNome(string nome);

        Task<bool> PossuiProdutos(Guid categoriaId);

        Task<bool> ExisteNome(string nome);

    }
}
