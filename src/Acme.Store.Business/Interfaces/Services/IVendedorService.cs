using Acme.Store.Abstractions.Interfaces.Services;
using Acme.Store.Business.Models;
using Acme.Store.Business.Validators;

namespace Acme.Store.Business.Interfaces.Services
{
    public interface IVendedorService : IBaseService<VendedorValidator, Vendedor>
    {
        Task Adicionar(Vendedor vendedor, string senha, bool emailConfirmed = false);

        Task Atualizar(Vendedor vendedor);

        Task Remover(Guid vendedorId);
    }
}
