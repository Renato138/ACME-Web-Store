using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Api.ViewModels;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Business.Interfaces;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Models;
using Acme.Store.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Acme.Store.Api.Controllers
{
    [Authorize]
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService produtoService,
                                  IProdutoRepository produtoRepository,
                                  IMapper mapper,
                                  ILogger<ProdutosController> logger,
                                  INotificador notificador,
                                  IAspNetUser aspNetUser) : base(notificador, logger, aspNetUser)
        {
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProdutoExibirViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoExibirViewModel>>(await _produtoRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoExibirViewModel>> ObterPorId(Guid id)
        {
            if (!_aspNetUser.IsAuthenticated)
                return Unauthorized();

            var produto = await _produtoService.ObterPorId(_aspNetUser, id);

            if (produto == null)
            {
                if (OperacaoValida())
                    return NotFound();
                else
                    return CustomResponse(null);
            }

            return _mapper.Map<ProdutoExibirViewModel>(produto);
        }

        [AllowAnonymous]
        [HttpGet("obter-por-categoria/{categoriaId:guid}")]
        public async Task<IEnumerable<ProdutoExibirViewModel>> ObterPorCategoria(Guid categoriaId)
        {
            return _mapper.Map<IEnumerable<ProdutoExibirViewModel>>(await _produtoService.ObterPorCategoria(categoriaId));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(ProdutoIncluirViewModel produtoViewModel)
        {
            if (!_aspNetUser.IsAuthenticated)
                return Unauthorized();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var imagemBase64 = produtoViewModel.ImagemBase64;
            var produto = _mapper.Map<Produto>(produtoViewModel);

            await _produtoService.Adicionar(_aspNetUser, produto, imagemBase64);

            return CustomResponse(_mapper.Map<ProdutoIncluirViewModel>(produto));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, ProdutoEditarViewModel produtoViewModel)
        {
            if (!_aspNetUser.IsAuthenticated)
            {
                return Unauthorized();
            }

            if (id != produtoViewModel.Id)
            {
                NotificarErro("O id informado não é mesmo que foi informado na query.");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var imagemBase64 = produtoViewModel.ImagemBase64;
            var produto = _mapper.Map<Produto>(produtoViewModel);

            await _produtoService.Atualizar(_aspNetUser, produto, imagemBase64);

            return CustomResponse(_mapper.Map<ProdutoEditarViewModel>(produto));

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (!await _produtoService.Existe(id))
                return NotFound();

            await _produtoService.Remover(_aspNetUser, id);

            return CustomResponse();
        }

    }
}
