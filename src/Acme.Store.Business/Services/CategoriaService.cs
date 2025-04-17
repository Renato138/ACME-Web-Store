using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Services;
using Acme.Store.Business.Interfaces;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Models;
using Acme.Store.Business.Validators;

namespace Acme.Store.Data.Services
{
    public class CategoriaService : BaseService<CategoriaValidator, Categoria>, ICategoriaService
    {
        private ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository, 
                                INotificador notificador) : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Adicionar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidator(), categoria))
            {
                return;
            }

            if (await _categoriaRepository.ExisteNome(categoria.Nome))
            {
                Notificar("Já existe uma categoria cadastrada com este nome.");
                return;
            }

            if (categoria.Id == Guid.Empty)
            {
                categoria.Id = Guid.NewGuid();
            }

            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidator(), categoria))
            {
                return;
            }

            var cat = await _categoriaRepository.ObterPorNome(categoria.Nome);

            if (cat != null && cat.Id != categoria.Id)
            {
                Notificar("Já existe uma categoria cadastrada com este nome.");
                return;
            }

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Remover(Guid categoriaId)
        {
            if (await _categoriaRepository.PossuiProdutos(categoriaId))
            {
                Notificar("Não é possível excluir esta categoria, a mesma possui produtos associados.");
                return;
            }
            await _categoriaRepository.Remover(categoriaId);
        }

        public override void Dispose()
        {
            _categoriaRepository.Dispose();
        }
    }
}
