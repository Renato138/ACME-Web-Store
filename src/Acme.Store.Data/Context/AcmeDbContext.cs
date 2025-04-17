using Acme.Store.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Data.Context
{
    public class AcmeDbContext : DbContext
    {
        public AcmeDbContext(DbContextOptions<AcmeDbContext> options) : base(options)
        {
        }
        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Vendedor> Vendedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                        e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcmeDbContext).Assembly);

            //foreach (var relationShip in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationShip.DeleteBehavior = DeleteBehavior.ClientSetNull;
            //}
            //base.OnModelCreating(modelBuilder);
        }
    }

}
