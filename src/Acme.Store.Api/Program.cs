using Acme.Store.Api.Configuration;
using Acme.Store.FirstRun;
using Acme.Store.Data.Context;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning.ApiExplorer;

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
    //builder.Services.AddDbContext<AcmeDbContext>(o =>
    //    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    builder.Services.AddDbContext<AcmeDbContext>(o =>
        o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.AddIdentityConfig();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.ResolveDependencies();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseDbMigrationHelper();

app.Run();

