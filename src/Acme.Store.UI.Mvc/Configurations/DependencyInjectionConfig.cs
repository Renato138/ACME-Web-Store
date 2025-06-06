﻿using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Interfaces;
using Acme.Store.Data.Context;
using Acme.Store.Data.Repositories;
using Acme.Store.Data.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Options;
using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Notitications;
using Acme.Store.Auth.Models;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Auth.Context;
using Acme.Store.Auth.Services;

namespace Acme.Store.UI.Mvc.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfile)); ;

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddScoped<AcmeDbContext>();
            builder.Services.AddScoped<AcmeIdentityDbContext>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();

            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<INotificador, Notificador>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<IVendedorService, VendedorService>();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IAspNetUser, AspNetUser>();

            return builder;
        }
    }
}
