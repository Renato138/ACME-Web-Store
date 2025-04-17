using Acme.Store.Abstractions.Interfaces.Services;
using Acme.Store.Business.Models;
using Acme.Store.Business.Validators;
using Microsoft.AspNetCore.Http;

namespace Acme.Store.Business.Interfaces.Services
{
    public interface IProdutoService : IBaseService<ProdutoValidator, Produto>
    {
        Task Adicionar(Produto produto, string imagemBase64);

        Task Adicionar(Produto produto, IFormFile arquivoImagem);

        Task Atualizar(Produto produto, string imagemBase64);

        Task Atualizar(Produto produto, IFormFile arquivoImagem);
    }
}
