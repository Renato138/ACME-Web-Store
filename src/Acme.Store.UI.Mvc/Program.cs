using Acme.Store.Data.Context;
using Acme.Store.UI.Mvc.Configurations;
using Acme.Store.FirstRun;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Acme.Store.Auth.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder
    .AddMvcConfiguration()
    .AddIdentityConfiguration()
    .AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseMvcConfiguration()
   .UseDbMigrationHelper();

app.Run();
