using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Models;
using Acme.Store.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Acme.Store.Data.Repositories
{
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(AcmeDbContext db) : base(db)
        {
        }

        public async Task<bool> ExisteEmail(string email)
        {
            return await Db.Vendedores.AnyAsync(v => v.Email == email);
        }

        public async Task<Vendedor> ObterPorEmail(string email)
        {
            return await Db.Vendedores.AsNoTracking().FirstOrDefaultAsync(v => v.Email == email);
        }

        public async Task<bool> ExisteNome(string nome)
        {
            return await Db.Vendedores.AnyAsync(v => v.Nome == nome);
        }

        public async Task<Vendedor> ObterPorNome(string nome)
        {
            return await Db.Vendedores.AsNoTracking().FirstOrDefaultAsync(v => v.Nome == nome);
        }

        public async Task<bool> PossuiProdutos(Guid vendedorId)
        {
            return await Db.Produtos.AnyAsync(p => p.VendedorId == vendedorId);
        }
    }
}
