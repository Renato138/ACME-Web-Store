using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Services;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Business.Constants;
using Acme.Store.Business.Interfaces;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Models;
using Acme.Store.Business.Models.Info;
using Acme.Store.Business.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Data.Services
{
    public class ProdutoService : BaseService<ProdutoValidator, Produto>, IProdutoService
    {
        private const int MB = 1048576;

        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IVendedorRepository _vendedorRepository;

        public ProdutoService(IProdutoRepository produtoRepository,
                              ICategoriaRepository categoriaRepository,
                              IVendedorRepository vendedorRepository,
                              INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _vendedorRepository = vendedorRepository;
        }

        public async Task<IEnumerable<Produto>> ObterTodos(IAspNetUser aspNetUser)
        {
            if (aspNetUser.IsInRole(Roles.Admin))
                return await _produtoRepository.ObterTodos();
            else
                return await _produtoRepository.ObterPorVendedor(aspNetUser.GetUserId());
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoria(IAspNetUser aspNetUser, Guid categoriaId)
        {
            if (aspNetUser.IsInRole(Roles.Admin))
                return await _produtoRepository.ObterPorCategoria(categoriaId);
            else
                return await _produtoRepository.ObterPorCategoriaVendedor(aspNetUser.GetUserId(), categoriaId);
        }

        public async Task<Produto> ObterPorId(IAspNetUser aspNetUser, Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (aspNetUser.IsInRole(Roles.Admin))
                return produto;
            else
            {
                if (produto.VendedorId == aspNetUser.GetUserId())
                    return produto;
                else
                {
                    Notificar("Um vendedor só pode consultar/obter produtos pertencetes a si.");
                }
            }
            return null;
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _produtoRepository.Existe(id);
        }

        public async Task Adicionar(IAspNetUser aspNetUser, Produto produto, IFormFile arquivoImagem)
        {
            if (!ExecutarValidacao(new ProdutoValidator(), produto))
            {
                return;
            }

            if (await _produtoRepository.ExisteNome(produto.Nome))
            {
                Notificar("Já existe um produto cadastrado com este nome.");
                return;
            }

            if (arquivoImagem == null)
            {
                Notificar("A imagem do produto é obrigatória.");
                return;
            }

            if (arquivoImagem.Length == 0)
            {
                Notificar("A tamanho do arquivo da imagem do produto não pode ser 0 (zero).");
                return;
            }

            if (! aspNetUser.IsInRole(Roles.Admin))
            {
                if (produto.VendedorId != aspNetUser.GetUserId())
                {
                    Notificar("Id do vendedor do produto diferente do Id do usuário logado. Um vendedor só pode incluir produtos para si mesmo.");
                    return;
                }
            }

            using (var stream = new MemoryStream((int)arquivoImagem.Length))
            {
                await arquivoImagem.CopyToAsync(stream);

                var mensagem = string.Empty;
                if (!ImageService.IsImage(stream, out mensagem))
                {
                    Notificar(mensagem);
                    return;
                }

                Stream streamImagem;

                if (ImageService.IsBigger(stream, ProdutoModelInfo.DefaultImageSize))
                    streamImagem = await ImageService.Resize(stream, ProdutoModelInfo.DefaultImageSize);
                else
                    streamImagem = stream;

                produto.Imagem = await ImageService.ToBase64String(streamImagem);
            }

            if (produto.Id == Guid.Empty)
            {
                produto.Id = Guid.NewGuid();
            }

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Adicionar(IAspNetUser aspNetUser, Produto produto, string imagemBase64)
        {
            if (!ExecutarValidacao(new ProdutoValidator(), produto))
            {
                return;
            }

            if (await _produtoRepository.ExisteNome(produto.Nome))
            {
                Notificar("Já existe um produto cadastrado com este nome.");
                return;
            }

            if (string.IsNullOrWhiteSpace(imagemBase64))
            {
                Notificar("A imagem do produto é obrigatória.");
                return;
            }

            if (!aspNetUser.IsInRole(Roles.Admin))
            {
                if (produto.VendedorId != aspNetUser.GetUserId())
                {
                    Notificar("Id do vendedor do produto diferente do Id do usuário logado. Um vendedor só pode incluir produtos para si mesmo.");
                    return;
                }
            }

            using (var stream = new MemoryStream(Convert.FromBase64String(imagemBase64)))
            {
                var mensagem = string.Empty;
                if (!ImageService.IsImage(stream, out mensagem))
                {
                    Notificar(mensagem);
                    return;
                }

                Stream streamImagem;

                if (ImageService.IsBigger(stream, ProdutoModelInfo.DefaultImageSize))
                    streamImagem = await ImageService.Resize(stream, ProdutoModelInfo.DefaultImageSize);
                else
                    streamImagem = stream;

                produto.Imagem = await ImageService.ToBase64String(streamImagem);
            }

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(IAspNetUser aspNetUser, Produto produto, IFormFile arquivoImagem)
        {
            if (!ExecutarValidacao(new ProdutoValidator(), produto))
            {
                return;
            }

            var prodNome = await _produtoRepository.ObterPorNome(produto.Nome);
            if (prodNome != null && prodNome.Id != produto.Id)
            {
                Notificar("Já existe um produto cadastrado com este nome.");
                return;
            }

            var prodOriginal = await _produtoRepository.ObterPorId(produto.Id);
            if (prodOriginal == null)
            {
                Notificar("Produto não localizado, não é possível atualizar.");
                return;
            }

            if (!aspNetUser.IsInRole(Roles.Admin))
            {
                if (prodOriginal.VendedorId != aspNetUser.GetUserId())
                {
                    Notificar("Id do vendedor do produto originalmente cadastrado diferente do Id do usuário logado. Um vendedor só pode atualizar produtos pertencentes a si mesmo.");
                    return;
                }
                if (produto.VendedorId != aspNetUser.GetUserId())
                {
                    Notificar("Id do vendedor do produto diferente do Id do usuário logado. Um vendedor só pode incluir produtos pertencentes a si mesmo.");
                    return;
                }
            }

            if (arquivoImagem == null || arquivoImagem.Length == 0)
            {
                produto.Imagem = prodOriginal.Imagem;
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    await arquivoImagem.CopyToAsync(stream);

                    var mensagem = string.Empty;
                    if (!ImageService.IsImage(stream, out mensagem))
                    {
                        Notificar(mensagem);
                        return;
                    }

                    Stream streamImagem;

                    if (ImageService.IsBigger(stream, ProdutoModelInfo.DefaultImageSize))
                        streamImagem = await ImageService.Resize(stream, ProdutoModelInfo.DefaultImageSize);
                    else
                        streamImagem = stream;

                    produto.Imagem = await ImageService.ToBase64String(streamImagem);
                }

            }
            await _produtoRepository.Atualizar(produto);
        }

        public async Task Atualizar(IAspNetUser aspNetUser, Produto produto, string imagemBase64)
        {
            if (!ExecutarValidacao(new ProdutoValidator(), produto))
            {
                return;
            }

            var prodNome = await _produtoRepository.ObterPorNome(produto.Nome);
            if (prodNome != null && prodNome.Id != produto.Id)
            {
                Notificar("Já existe um produto cadastrado com este nome.");
                return;
            }

            var prodOriginal = await _produtoRepository.ObterPorId(produto.Id);
            if (prodOriginal == null)
            {
                Notificar("Produto não localizado, não é possível atualizar.");
                return;
            }

            if (!aspNetUser.IsInRole(Roles.Admin))
            {
                if (prodOriginal.VendedorId != aspNetUser.GetUserId())
                {
                    Notificar("Id do vendedor do produto originalmente cadastrado diferente do Id do usuário logado. Um vendedor só pode atualizar produtos pertencentes a si mesmo.");
                    return;
                }
                if (produto.VendedorId != aspNetUser.GetUserId())
                {
                    Notificar("Id do vendedor do produto diferente do Id do usuário logado. Um vendedor só pode incluir produtos pertencentes a si mesmo.");
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(imagemBase64))
            {
                produto.Imagem = prodOriginal.Imagem;
            }
            else
            {
                using (var stream = new MemoryStream(Convert.FromBase64String(imagemBase64)))
                {
                    var mensagem = string.Empty;
                    if (!ImageService.IsImage(stream, out mensagem))
                    {
                        Notificar(mensagem);
                        return;
                    }

                    Stream streamImagem;

                    if (ImageService.IsBigger(stream, ProdutoModelInfo.DefaultImageSize))
                        streamImagem = await ImageService.Resize(stream, ProdutoModelInfo.DefaultImageSize);
                    else
                        streamImagem = stream;

                    produto.Imagem = await ImageService.ToBase64String(streamImagem);
                }

            }
            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover(IAspNetUser aspNetUser, Guid produtoId)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);
            if (produto == null)
            {
                Notificar("Produto não localizado, não é possível excluir.");
                return;
            }

            if (!aspNetUser.IsInRole(Roles.Admin))
            {
                if (produto.VendedorId != aspNetUser.GetUserId())
                {
                    Notificar("Id do vendedor do produto diferente do Id do usuário logado. Um vendedor só pode excluir produtos pertencentes a si mesmo.");
                    return;
                }
            }

            await _produtoRepository.Remover(produtoId);
        }
        public override void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
