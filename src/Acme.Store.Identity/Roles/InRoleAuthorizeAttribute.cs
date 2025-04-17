using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Auth.Roles
{

    public class InRoleAuthorizeAttribute : TypeFilterAttribute
    {
        public InRoleAuthorizeAttribute(string roleName) : base(typeof(InRoleFilter))
        {
            Arguments = new object[] { roleName };
        }
    }

}
