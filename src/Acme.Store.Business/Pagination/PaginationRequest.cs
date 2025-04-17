using Acme.Store.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Business.Pagination
{
    public record PaginationRequest(int PageNum, int PageSize);
    
}
