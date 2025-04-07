using Acme.Store.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Acme.Store.Business.Pagination;

namespace Acme.Store.Business.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);

        Task Atualizar(TEntity entity);

        Task Remover(Guid id);

        Task<IEnumerable<TEntity>> ObterTodos();

        Task<PaginationReponse<TEntity>> ObterPagina(PaginationRequest paginationRequest);

        Task<TEntity> ObterPorId(Guid id);

        Task<bool> Existe(Guid id);

        Task<int> SaveChanges();
    }
}
