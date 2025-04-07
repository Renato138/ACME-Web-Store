using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;

namespace Acme.Store.Data.TypeConfigurations
{
    public class VendedorTypeConfiguration : IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Nome)
                .IsRequired()
                .HasColumnType($"varchar({VendedorModelInfo.NomeMaxLength})");

            builder.Property(v => v.Email)
                .IsRequired()
                .HasColumnType($"varchar({VendedorModelInfo.EmailMaxLength})");

            //builder.Property(v => v.DataHoraCriacao)
            //    .IsRequired()
            //    .HasDefaultValueSql("GETDATE()")
            //    .HasColumnType("datetime");

            //builder.Property(v => v.DataHoraUltimaAtualizacao)
            //    .IsRequired()
            //    .HasDefaultValueSql("GETDATE()")
            //    .HasColumnType("datetime");

            // Relação 1 : N -> Vendedor => Produto
            builder.HasMany(v => v.Produtos)
                .WithOne(p => p.Vendedor)
                .HasForeignKey(p => p.VendedorId);

            builder.ToTable("Vendedores", "dbo");
        }
    }

}
