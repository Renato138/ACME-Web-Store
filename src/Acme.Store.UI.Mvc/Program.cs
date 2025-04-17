using Acme.Store.Data.Context;
using Acme.Store.UI.Mvc.Configurations;
using Acme.Store.UI.Mvc.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder
    .AddMvcConfiguration()
    .AddIdentityConfiguration()
    .AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseMvcConfiguration();

app.UseDbMigrationHelper();

app.Run();
