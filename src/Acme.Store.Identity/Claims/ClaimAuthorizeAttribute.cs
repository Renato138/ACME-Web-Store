using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Auth.Claims
{

    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimAuthorizationFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

}
