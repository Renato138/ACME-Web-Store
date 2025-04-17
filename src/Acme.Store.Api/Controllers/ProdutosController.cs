using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Api.ViewModels;
using Acme.Store.Business.Interfaces;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Store.Api.Controllers
{
    [Authorize]
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IProdutoService produtoService,
                                  IMapper mapper,
                                  ILogger<ProdutosController> logger,
                                  INotificador notificador) : base(notificador, logger)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoExibirViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoExibirViewModel>>(await _produtoRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoExibirViewModel>> ObterPorId(Guid id)
        {
            var produto = _mapper.Map<ProdutoExibirViewModel>(await _produtoRepository.ObterPorId(id));

            if (produto == null)
                return NotFound();

            return _mapper.Map<ProdutoExibirViewModel>(produto);
        }

        [HttpGet("obter-por-vendedor/{vendedorId:guid}")]
        public async Task<IEnumerable<ProdutoExibirViewModel>> ObterPorVendedor(Guid vendedorId)
        {
            return _mapper.Map<IEnumerable<ProdutoExibirViewModel>>(await _produtoRepository.ObterPorVendedor(vendedorId));
        }

        [HttpGet("obter-por-categoria/{categoriaId:guid}")]
        public async Task<IEnumerable<ProdutoExibirViewModel>> ObterPorPCategoria(Guid categoriaId)
        {
            return _mapper.Map<IEnumerable<ProdutoExibirViewModel>>(await _produtoRepository.ObterPorCategoria(categoriaId));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(ProdutoIncluirViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var imagemBase64 = produtoViewModel.ImagemBase64;
            var produto = _mapper.Map<Produto>(produtoViewModel);

            await _produtoService.Adicionar(produto, imagemBase64);

            return CustomResponse(_mapper.Map<ProdutoIncluirViewModel>(produto));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, ProdutoEditarViewModel produtoViewModel)
        {
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

            await _produtoService.Atualizar(produto, imagemBase64);

            return CustomResponse(_mapper.Map<ProdutoEditarViewModel>(produto));

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (!await _produtoRepository.Existe(id))
                return NotFound();

            await _produtoRepository.Remover(id);

            return CustomResponse();
        }

    }
}
