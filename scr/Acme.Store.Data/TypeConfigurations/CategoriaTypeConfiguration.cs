using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;

namespace Acme.Store.Data.TypeConfigurations
{
    public class CategoriaTypeConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType($"varchar({CategoriaModelInfo.NomeMaxLength})");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType($"varchar({CategoriaModelInfo.DescricaoMaxLength})");

            //builder.Property(c => c.DataHoraCriacao)
            //    .IsRequired()
            //    .HasDefaultValueSql("GETDATE()")
            //    .HasColumnType("datetime");

            //builder.Property(c => c.DataHoraUltimaAtualizacao)
            //    .IsRequired()
            //    .HasDefaultValueSql("GETDATE()")
            //    .HasColumnType("datetime");

            // Relação 1 : N -> Categoria => Produto
            builder.HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);

            builder.ToTable("Categorias", "dbo");
        }
    }

}
