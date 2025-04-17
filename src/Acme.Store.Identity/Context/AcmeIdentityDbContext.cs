using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Auth.Context
{
    public class AcmeIdentityDbContext : IdentityDbContext
    {
        public AcmeIdentityDbContext(DbContextOptions<AcmeIdentityDbContext> options) : base(options)
        {
        }
    }
}
