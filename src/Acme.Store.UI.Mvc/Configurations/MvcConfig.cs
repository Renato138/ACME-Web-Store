using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System;
using Acme.Store.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Acme.Store.UI.Mvc.Configurations
{
    public static class MvcConfig
    {
        public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

            builder.Services.AddResponseCaching();

            builder.Services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookieValue = "true";
            });

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<AcmeDbContext>(o =>
                    o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSQLite")));
                //builder.Services.AddDbContext<AcmeDbContext>(o =>
                //    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                builder.Services.AddDbContext<AcmeDbContext>(o =>
                    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            return builder;
        }

        public static WebApplication UseMvcConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }

            app.UseResponseCaching();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            return app;
        }
    }
}
