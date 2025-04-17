using Acme.Store.Abstractions.Interfaces.Repositories;
using Acme.Store.Business.Models;

namespace Acme.Store.Business.Interfaces.Repositories
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        Task<bool> ExisteEmail(string email);

        Task<Vendedor> ObterPorEmail(string email);

        Task<bool> ExisteNome(string nome);

        Task<Vendedor> ObterPorNome(string nome);

        Task<bool> PossuiProdutos(Guid vendedorId);

    }
}
