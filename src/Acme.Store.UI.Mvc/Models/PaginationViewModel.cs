using Acme.Store.Business.Pagination;

namespace Acme.Store.UI.Mvc.Models
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public string Controller {  get; set; }
    }
}
