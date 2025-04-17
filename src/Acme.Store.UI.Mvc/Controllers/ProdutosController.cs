using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Business.Interfaces;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Models;
using Acme.Store.Business.Pagination;
using Acme.Store.UI.Mvc.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Store.UI.Mvc.Controllers
{
    [Route("produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService produtoService,
                                  IProdutoRepository produtoRepository,
                                  ICategoriaRepository categoriaRepository,
                                  IVendedorRepository vendedorRepository,
                                  IMapper mapper,
                                  INotificador notificador, 
                                  ILogger<MainController> logger) : base(notificador, logger)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _categoriaRepository = categoriaRepository;
            _vendedorRepository = vendedorRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var produtos = _mapper.Map<IEnumerable<ProdutoExibirViewModel>>(await _produtoRepository.ObterTodos())
                                .OrderBy(p => p.Nome);

            return View(produtos);

        }

        [Route("detalhes/{id:guid?}")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty)
            {
                return NotFound();
            }

            var produto = await _produtoRepository.ObterPorId(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProdutoExibirViewModel>(produto));
        }

        [Route("criar-novo")]
        public async Task<ActionResult> Create()
        {
            var produtoVM = new ProdutoIncluirViewModel();

            produtoVM.CategoriasSelectList = await ObterCategoriasSelectList(produtoVM.CategoriaId);
            produtoVM.VendedoresSelectList = await ObterVendedoresSelectList(produtoVM.VendedorId);

            return View(produtoVM);
        }

        // POST: ProdutosController/Create
        [ValidateAntiForgeryToken]
        [HttpPost("criar-novo")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,CategoriaId,VendedorId,Preco,QuantidadeEstoque,UnidadeVenda,ImagemUpload")] ProdutoIncluirViewModel produto)
        {
            if (ModelState.IsValid)
            {
                if (produto == null)
                {
                    return NotFound();
                }

                var formFile = produto.ImagemUpload;
                var prod = _mapper.Map<Produto>(produto);

                await _produtoService.Adicionar(prod, formFile);

                if (OperacaoValida())
                {
                    return RedirectToAction("Index");
                }
                produto.ImagemUpload = formFile;
            }

            produto.CategoriasSelectList = await ObterCategoriasSelectList(produto.CategoriaId);
            produto.VendedoresSelectList = await ObterVendedoresSelectList(produto.CategoriaId);

            return View(produto);
        }

        // GET: ProdutosController/Edit/5
        [Route("editar/{id:guid}")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var produto = await _produtoRepository.ObterPorId(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            var produtoVM = _mapper.Map<ProdutoEditarViewModel>(produto);
            produtoVM.CategoriasSelectList = await ObterCategoriasSelectList(produtoVM.CategoriaId);
            produtoVM.VendedoresSelectList = await ObterVendedoresSelectList(produtoVM.VendedorId);

            return View(produtoVM);
        }

        // POST: ProdutosController/Edit/5
        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid? id, [Bind("Id,Nome,Descricao,CategoriaId,VendedorId,Preco,QuantidadeEstoque,UnidadeVenda,ImagemUpload")] ProdutoEditarViewModel produto)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var formFile = produto.ImagemUpload;
                var prod = _mapper.Map<Produto>(produto);

                await _produtoService.Atualizar(prod, formFile);

                if (OperacaoValida())
                {
                    return RedirectToAction(nameof(Index));
                }
                produto.ImagemUpload = formFile;
            }

            produto.CategoriasSelectList = await ObterCategoriasSelectList(produto.CategoriaId);
            produto.VendedoresSelectList = await ObterVendedoresSelectList(produto.VendedorId);

            return View(produto);
        }

        // GET: ProdutosController/Delete/5
        [Route("excluir/{id:guid}")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var produto = _mapper.Map<ProdutoExibirViewModel>(await _produtoRepository.ObterPorId(id.Value));
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: ProdutosController/Delete/5
        [HttpPost("excluir/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid? id, [Bind("Id,Nome,Descricao,CategoriaId,VendedorId,Preco,QuantidadeEstoque,UnidadeVenda,ImagemUpload")] ProdutoEditarViewModel produto)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            await _produtoRepository.Remover(id.Value);

            return RedirectOrReturn("Index", "Delete", produto);
        }

        private async Task<SelectList> ObterCategoriasSelectList(Guid? selectedId)
        {
            var items = new List<SelectListViewModel>()
            {
                new SelectListViewModel()
                {
                    Id = null,
                    Nome = "Selecione uma categoria..."
                }
            };

            items.AddRange(_mapper.Map<IEnumerable<SelectListViewModel>>((await _categoriaRepository.ObterTodos()).OrderBy(c => c.Nome)));
            return new SelectList(items, "Id", "Nome");
        }

        private async Task<SelectList> ObterVendedoresSelectList(Guid? selectedId)
        {
            var items = new List<SelectListViewModel>() 
            { 
                new SelectListViewModel() 
                {
                    Id = null,
                    Nome = "Selecione um vendedor..."
                }
            };

            items.AddRange(_mapper.Map<IEnumerable<SelectListViewModel>>((await _vendedorRepository.ObterTodos()).OrderBy(c => c.Nome)));
            return new SelectList(items, "Id", "Nome", selectedId);
        }
    }
}
