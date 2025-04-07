using Acme.Store.Business.Models;

namespace Acme.Store.Business.Pagination
{
    public record PaginationReponse<TEntity>(
        PaginationRequest PaginationRequest, 
        IEnumerable<TEntity> Items,
        int TotalItems)
    {
        public int TatalPages { get; } = TotalItems / PaginationRequest.PageSize + ((TotalItems / PaginationRequest.PageSize) == 0 ? 0 : 1 );
    }
}
