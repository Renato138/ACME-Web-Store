using Acme.Store.Data.Context;
using Microsoft.AspNetCore.Identity;
using System;

namespace Acme.Store.UI.Mvc.Configurations
{
    public static class IdentityConfig
    {
        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AcmeDbContext>();

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
