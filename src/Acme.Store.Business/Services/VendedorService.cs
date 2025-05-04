using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Services;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Auth.Models;
using Acme.Store.Auth.Validators;
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
        private IUsuarioService _usuarioService;

        public VendedorService(IVendedorRepository vendedorRepository,
                               IUsuarioService usuarioService,
                               INotificador notificador) : base(notificador)
        {
            _vendedorRepository = vendedorRepository;
            _usuarioService = usuarioService;
        }

        public async Task Adicionar(Vendedor vendedor, string senha, bool emailConfirmed = false)
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

            var usuario = new Usuario
            {
                //Nome = vendedor.Nome,
                Email = vendedor.Email,
                Senha = senha
            };

            if (! _usuarioService.ExecutarValidacao(new UsuarioValidator(), usuario))
            {
                return;
            }

            var user = await _usuarioService.Adicionar(usuario, emailConfirmed);
            if (user == null)
            {
                return;
            }

            vendedor.Id = Guid.Parse(user.Id);
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

            // Obtem o usuário associado ao vendedor
            var user = await _usuarioService.ObterPorId(vendedor.Id);
            if (user == null)
            {
                Notificar("Usuário do Identity não localizado para este vendedor.");
                return;
            }

            // Se o email do vendendedor for diferente do e-mail do usuário
            // significa o e-mail do vendedor foi alterado.
            // Então verifica se o novo e-mail está disponível
            if (user.Email.ToLower() != vendedor.Email.ToLower())
            {
                if (!await _usuarioService.ValidarDisponibilidadeEmail(vendedor.Id, vendedor.Email))
                {
                    return;
                }
            }

            // Se o nome do vendendedor for diferente do nome do usuário
            // significa o nome do vendedor foi alterado.
            // Então verifica se o novo nome está disponível
            //if (user.UserName.ToLower() != vendedor.Nome.ToLower())
            //{
            //    if (!await _usuarioService.ValidarDisponibilidadeNome(vendedor.Id, vendedor.Nome))
            //    {
            //        return;
            //    }
            //}

            await _vendedorRepository.Atualizar(vendedor);
            await _usuarioService.Atualizar(vendedor.Id, vendedor.Email);
        }

        public async Task Remover(Guid vendedorId)
        {
            if (await _vendedorRepository.PossuiProdutos(vendedorId))
            {
                Notificar("Não é possível excluir este vendedor, o mesmo possui produtos associados.");
                return;
            }

            // Obtem o usuário associado ao vendedor
            var user = await _usuarioService.ObterPorId(vendedorId);
            if (user == null)
            {
                Notificar("Usuário do Identity não localizado para este vendedor.");
                return;
            }

            await _vendedorRepository.Remover(vendedorId);
            await _usuarioService.Remover(vendedorId);
        }

        public override void Dispose()
        {
            _vendedorRepository.Dispose();
        }
    }
}
