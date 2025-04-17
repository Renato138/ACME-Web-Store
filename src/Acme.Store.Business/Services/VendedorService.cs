using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Services;
using Acme.Store.Business.Interfaces;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Models;
using Acme.Store.Business.Validators;

namespace Acme.Store.Data.Services
{
    public class VendedorService : BaseService<VendedorValidator, Vendedor>, IVendedorService
    {
        private IVendedorRepository _vendedorRepository;

        public VendedorService(IVendedorRepository vendedorRepository,
                               INotificador notificador) : base(notificador)
        {
            _vendedorRepository = vendedorRepository;
        }

        public async Task Adicionar(Vendedor vendedor)
        {
            if (!ExecutarValidacao(new VendedorValidator(), vendedor))
            {
                return;
            }

            if (await _vendedorRepository.ExisteNome(vendedor.Nome))
            {
                Notificar("Já existe um vendedor cadastrado com este nome.");
                return;
            }

            if (await _vendedorRepository.ExisteEmail(vendedor.Email))
            {
                Notificar("Já existe um vendedor cadastrado com este e-mail.");
                return;
            }

            await _vendedorRepository.Adicionar(vendedor);
        }

        public async Task Atualizar(Vendedor vendedor)
        {
            if (!ExecutarValidacao(new VendedorValidator(), vendedor))
            {
                return;
            }

            var vend = await _vendedorRepository.ObterPorNome(vendedor.Nome);
            if (vend != null && vend.Id != vendedor.Id)
            {
                Notificar("Já existe um vendedor cadastrado com este nome.");
                return;
            }

            vend = await _vendedorRepository.ObterPorEmail(vendedor.Email);
            if (vend != null && vend.Id != vendedor.Id)
            {
                Notificar("Já existe um vendedor cadastrado com este e-mail.");
                return;
            }

            await _vendedorRepository.Atualizar(vendedor);
        }

        public async Task Remover(Guid vendedorId)
        {
            if (await _vendedorRepository.PossuiProdutos(vendedorId))
            {
                Notificar("Não é possível excluir este vendedor, o mesmo possui produtos associados.");
                return;
            }
            await _vendedorRepository.Remover(vendedorId);
        }

        public override void Dispose()
        {
            _vendedorRepository.Dispose();
        }
    }
}
