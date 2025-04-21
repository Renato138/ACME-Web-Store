using Acme.Store.Api.Configuration;
using Acme.Store.Api.Extensions;
using Acme.Store.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// ConfigureServices

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AcmeDbContext>(o =>
        o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSQLite")));
}
else
{
    builder.Services.AddDbContext<AcmeDbContext>(o =>
        o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.AddIdentityConfig();

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.ResolveDependencies();

var app = builder.Build();

// Configure

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig();

app.UseDbMigrationHelper();

app.Run();

