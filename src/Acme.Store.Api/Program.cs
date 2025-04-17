using Acme.Store.Api.Configuration;
using Acme.Store.Api.Extensions;
using Acme.Store.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddIdentityConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseDbMigrationHelper();

app.Run();
