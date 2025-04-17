using Acme.Store.Auth.Context;
using Acme.Store.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Acme.Store.UI.Mvc.Configurations
{
    public static class IdentityConfig
    {
        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<AcmeIdentityDbContext>(o =>
                    o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSQLite")));
                //builder.Services.AddDbContext<AcmeIdentityDbContext>(o =>
                //    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                builder.Services.AddDbContext<AcmeIdentityDbContext>(o =>
                    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AcmeIdentityDbContext>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluirPermanentemente", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("VerProdutos", policy =>
                    policy.RequireClaim("Produtos", "VI"));
            });

            return builder;
        }
    }
}
