using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Services;
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

        public async Task Adicionar(Produto produto, IFormFile arquivoImagem)
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

        public async Task Adicionar(Produto produto, string imagemBase64)
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

        public async Task Atualizar(Produto produto, IFormFile arquivoImagem)
        {
            if (!ExecutarValidacao(new ProdutoValidator(), produto))
            {
                return;
            }

            var prod = await _produtoRepository.ObterPorNome(produto.Nome);

            if (prod != null && prod.Id != produto.Id)
            {
                Notificar("Já existe uma categoria cadastrada com este nome.");
                return;
            }

            if (arquivoImagem == null || arquivoImagem.Length == 0)
            {
                prod = await _produtoRepository.ObterPorId(produto.Id);
                produto.Imagem = prod.Imagem;
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

        public async Task Atualizar(Produto produto, string imagemBase64)
        {
            if (!ExecutarValidacao(new ProdutoValidator(), produto))
            {
                return;
            }

            var prod = await _produtoRepository.ObterPorNome(produto.Nome);

            if (prod != null && prod.Id != produto.Id)
            {
                Notificar("Já existe uma categoria cadastrada com este nome.");
                return;
            }

            if (string.IsNullOrWhiteSpace(imagemBase64))
            {
                prod = await _produtoRepository.ObterPorId(produto.Id);
                produto.Imagem = prod.Imagem;
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

        public override void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
