using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Auth.Interfaces
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
