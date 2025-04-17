using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Models;
using Acme.Store.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Acme.Store.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AcmeDbContext db) : base(db)
        {
        }

        public async Task<bool> ExisteNome(string nome)
        {
            return await Db.Categorias.AnyAsync(e => e.Nome == nome);
        }

        public async Task<Categoria> ObterPorNome(string nome)
        {
            return await Db.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task<bool> PossuiProdutos(Guid categoriaId)
        {
            return await Db.Produtos.AnyAsync(p => p.CategoriaId == categoriaId);
        }
    }
}
