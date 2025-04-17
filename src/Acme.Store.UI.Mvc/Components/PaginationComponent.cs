using Acme.Store.Business.Pagination;
using Acme.Store.UI.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Store.UI.Mvc.Components
{
    public class PaginationComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int currentPage, int itemsPerPage, int totalItems, string aspController)
        {
            var pagViewModel = new PaginationViewModel
            {
                CurrentPage = currentPage,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems,
                TotalPages = totalItems / itemsPerPage + ((totalItems % itemsPerPage) == 0 ? 0 : 1),
                Controller = aspController
            };

            return View(pagViewModel);
        }
    }
}
