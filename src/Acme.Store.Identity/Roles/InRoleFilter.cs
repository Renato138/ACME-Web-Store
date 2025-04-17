using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Acme.Store.Auth.Roles
{
    public class InRoleFilter : IAuthorizationFilter
    {
        private readonly string _roleName;

        public InRoleFilter(string roleName)
        {
            _roleName = roleName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (! context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (! context.HttpContext.User.IsInRole(_roleName))
            {
                context.Result = new StatusCodeResult(403);
            }

        }
    }

}
