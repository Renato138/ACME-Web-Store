using Acme.Store.Auth.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Acme.Store.Data.Context;
using SixLabors.ImageSharp;
using Acme.Store.Auth.Token;
using Microsoft.IdentityModel.Tokens;

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
                //builder.Services.AddDbContext<AcmeIdentityDbContext>(o =>
                //    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                builder.Services.AddDbContext<AcmeIdentityDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }


            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AcmeIdentityDbContext>()
                .AddDefaultTokenProviders();

            // JWT

            var tokenSettingsSection = builder.Configuration.GetSection("TokenSettings");
            builder.Services.Configure<TokenSettings>(tokenSettingsSection);

            var tokenSettings = tokenSettingsSection.Get<TokenSettings>();
            var key = Encoding.UTF8.GetBytes(tokenSettings.Secret);
            var secureKey = new SymmetricSecurityKey(key);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = tokenSettings.ValidoEm,
                    ValidIssuer = tokenSettings.Emissor
                };
            });

            //builder.Services.AddAuthorization();

            return builder;
        }
    }

}
