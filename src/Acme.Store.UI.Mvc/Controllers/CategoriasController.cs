using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Acme.Store.Data.Repositories;
using Acme.Store.UI.Mvc.Models;
using Acme.Store.Business.Models;
using Acme.Store.Data.Services;
using Acme.Store.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Acme.Store.Auth.Interfaces;

namespace Acme.Store.UI.Mvc.Controllers
{
    [Authorize]
    [Route("categorias")]
    public class CategoriasController : MainController
    {
        private readonly ICategoriaService _categoriaService;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaService categoriaService,
                                    ICategoriaRepository categoriaRepository,
                                    IMapper mapper,
                                    INotificador notificador,
                                    ILogger<MainController> logger,
                                    IAspNetUser aspNetUser) : base(notificador, logger, aspNetUser)
        {
            _categoriaService = categoriaService;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos())
                                .OrderBy(p => p.Nome);

            return View(categorias);
        }

        [Route("detalhes/{id:guid?}")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty)
            {
                return NotFound();
            }

            var categoria = await _categoriaRepository.ObterPorId(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CategoriaViewModel>(categoria));
        }

        [Route("criar-novo")]
        public async Task<ActionResult> Create()
        {
            var categoria = _mapper.Map<CategoriaViewModel>(new Categoria());

            return View(categoria);
        }

        [HttpPost("criar-novo")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Nome,Descricao")] CategoriaViewModel categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria == null)
                {
                    return NotFound();
                }

                await _categoriaService.Adicionar(_mapper.Map<Categoria>(categoria));

                return RedirectOrReturn(nameof(Index), nameof(Create), categoria);

            }
            return View(categoria);
        }

        [Route("editar/{id:guid}")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorId(id.Value));
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid? id, [Bind("Id,Nome,Descricao")] CategoriaViewModel categoria)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoriaService.Atualizar(_mapper.Map<Categoria>(categoria));

                return RedirectOrReturn(nameof(Index), nameof(Edit), categoria);
            }

            return View(categoria);
        }

        [Route("excluir/{id:guid}")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorId(id.Value));
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost("excluir/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid? id, [Bind("Id,Nome,Descricao")] CategoriaViewModel categoria)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            await _categoriaRepository.Remover(id.Value);

            return RedirectOrReturn("Index", "Delete", categoria);
        }
    }
}
