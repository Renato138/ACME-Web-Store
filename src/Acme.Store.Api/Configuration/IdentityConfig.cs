using Acme.Store.Auth.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Acme.Store.Data.Context;
using SixLabors.ImageSharp;

namespace Acme.Store.Api.Configuration
{
    public static class IdentityConfig
    {
        public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<AcmeIdentityDbContext>(o =>
                    o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSQLite")));
            }
            else
            {
                builder.Services.AddDbContext<AcmeIdentityDbContext>(o =>
                    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }

            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AcmeIdentityDbContext>()
                .AddDefaultTokenProviders();

            // JWT

            //var appSettingsSection = configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            //var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = true;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = appSettings.ValidoEm,
            //        ValidIssuer = appSettings.Emissor
            //    };
            //});

            return builder;
        }
    }
}
