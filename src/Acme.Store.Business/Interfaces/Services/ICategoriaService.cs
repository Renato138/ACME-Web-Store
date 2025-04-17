using Acme.Store.Abstractions.Interfaces.Services;
using Acme.Store.Business.Models;
using Acme.Store.Business.Validators;

namespace Acme.Store.Business.Interfaces.Services
{
    public interface ICategoriaService : IBaseService<CategoriaValidator, Categoria>
    {
        Task Adicionar(Categoria categoria);

        Task Atualizar(Categoria categoria);

        Task Remover(Guid categoriaId);
    }
}
