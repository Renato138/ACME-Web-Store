using Acme.Store.Abstractions.Interfaces.Repositories;
using Acme.Store.Abstractions.Models;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Models;
using Acme.Store.Business.Pagination;
using Acme.Store.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Acme.Store.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected AcmeDbContext Db;

        protected DbSet<TEntity> DbSet => Db.Set<TEntity>();

        protected IQueryable<TEntity> DbSetNoTracking => Db.Set<TEntity>().AsNoTracking();

        public Repository(AcmeDbContext db)
        {
            Db = db;
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await DbSetNoTracking.ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSetNoTracking.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<bool> Existe(Guid id)
        {
            return await DbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        public async Task<PaginationReponse<TEntity>> ObterPagina(PaginationRequest paginationRequest)
        {
            var skip = (paginationRequest.PageNum - 1) + paginationRequest.PageSize;
            var totalRecords = DbSet.Count();
            var items = await DbSet.Skip(skip).Take(paginationRequest.PageSize).ToListAsync();

            var paginationResponse = new PaginationReponse<TEntity>(paginationRequest, items, totalRecords);

            return paginationResponse;
        }
    }

}
