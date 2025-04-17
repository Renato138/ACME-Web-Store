using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Models;
using Acme.Store.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AcmeDbContext db) : base(db)
        {
        }

        public async Task<bool> ExisteNome(string nome)
        {
            return await Db.Produtos.AnyAsync(e => e.Nome == nome);
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoria(Guid categoriaId)
        {
            return await Db.Produtos
                .AsNoTracking()
                .Where(p => p.CategoriaId == categoriaId)
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoriaVendedor(Guid categoriaId, Guid vendedorId)
        {
            return await Db.Produtos.AsNoTracking().Where(p => p.CategoriaId == categoriaId && p.VendedorId == vendedorId).ToListAsync();
        }

        public async Task<Produto> ObterPorNome(string nome)
        {
            return await Db.Produtos
                .AsNoTracking()
                .Where(p => p.Nome == nome)
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Produto>> ObterPorVendedor(Guid vendedorId)
        {
            return await Db.Produtos
                .AsNoTracking()
                .Where(p => p.VendedorId == vendedorId)
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await Db.Produtos
                .AsNoTracking()
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .ToListAsync();

        }

        public override async Task<Produto> ObterPorId(Guid id)
        {
            return await Db.Produtos
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .FirstOrDefaultAsync();
        }
    }
}
