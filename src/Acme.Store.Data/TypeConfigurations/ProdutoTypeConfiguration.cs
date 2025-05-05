using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;

namespace Acme.Store.Data.TypeConfigurations
{
    public class ProdutoTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType($"varchar({ProdutoModelInfo.NomeMaxLength})");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType($"varchar({ProdutoModelInfo.DescricaoMaxLength})");

            builder.Property(p => p.UnidadeVenda)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.Preco)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnType("money");

            builder.Property(p => p.QuantidadeEstoque)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnType("int");

            builder.Property(p => p.CategoriaId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(p => p.VendedorId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            //builder.Property(p => p.Imagem)
            //    .HasColumnType();

            //builder.Property(p => p.DataHoraCriacao)
            //    .IsRequired()
            //    .HasDefaultValueSql("GETDATE()")
            //    .HasColumnType("datetime");

            //builder.Property(p => p.DataHoraUltimaAtualizacao)
            //    .IsRequired()
            //    .HasDefaultValueSql("GETDATE()")
            //    .HasColumnType("datetime");

            // Relação 1 : N -> Vendedor => Produto
            builder.HasOne(p => p.Vendedor)
                .WithMany(v => v.Produtos)
                .HasForeignKey(p => p.VendedorId);

            // Relação 1 : N -> Categoria => Produto
            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);

            builder.ToTable("Produtos", "dbo");
        }
    }

}
