using Acme.Store.Abstractions.Interfaces.Services;
using Acme.Store.Abstractions.Models;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Business.Models;
using Acme.Store.Business.Validators;
using Microsoft.AspNetCore.Http;

namespace Acme.Store.Business.Interfaces.Services
{
    public interface IProdutoService : IBaseService<ProdutoValidator, Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos(IAspNetUser aspNetUser);

        Task<IEnumerable<Produto>> ObterPorCategoria(IAspNetUser aspNetUser, Guid categoriaId);

        Task<Produto> ObterPorId(IAspNetUser aspNetUser, Guid id);

        Task<bool> Existe(Guid produtoId);

        Task Adicionar(IAspNetUser aspNetUser, Produto produto, string imagemBase64);

        Task Adicionar(IAspNetUser aspNetUser, Produto produto, IFormFile arquivoImagem);

        Task Atualizar(IAspNetUser aspNetUser, Produto produto, string imagemBase64);

        Task Atualizar(IAspNetUser aspNetUser, Produto produto, IFormFile arquivoImagem);

        Task Remover(IAspNetUser aspNetUser, Guid produtoId);
    }
}
