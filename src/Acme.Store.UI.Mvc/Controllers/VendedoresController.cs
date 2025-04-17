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

namespace Acme.Store.UI.Mvc.Controllers
{
    [Route("vendedores")]
    public class VendedoresController : MainController
    {
        private readonly IVendedorService _vendedorService;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMapper _mapper;

        public VendedoresController(IVendedorService vendedorService,
                                    IVendedorRepository vendedorRepository,
                                    IMapper mapper,
                                    INotificador notificador,
                                    ILogger<MainController> logger) : base(notificador, logger)
        {
            _vendedorService = vendedorService;
            _vendedorRepository = vendedorRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var vendedores = _mapper.Map<IEnumerable<VendedorViewModel>>(await _vendedorRepository.ObterTodos())
                                .OrderBy(p => p.Nome);

            return View(vendedores);
        }

        [Route("detalhes/{id:guid?}")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty)
            {
                return NotFound();
            }

            var vendedor = await _vendedorRepository.ObterPorId(id.Value);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VendedorViewModel>(vendedor));
        }

        [Route("criar-novo")]
        public async Task<ActionResult> Create()
        {
            var vendedor = _mapper.Map<VendedorViewModel>(new Vendedor());

            return View(vendedor);
        }

        // POST: VendedoresController/Create
        [HttpPost("criar-novo")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Nome,Email")] VendedorViewModel vendedor)
        {
            if (ModelState.IsValid)
            {
                if (vendedor == null)
                {
                    return NotFound();
                }

                await _vendedorService.Adicionar(_mapper.Map<Vendedor>(vendedor));

                return RedirectOrReturn(nameof(Index), nameof(Create), vendedor);

            }
            return View(vendedor);
        }

        [Route("editar/{id:guid}")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var vendedor = _mapper.Map<VendedorViewModel>(await _vendedorRepository.ObterPorId(id.Value));
            if (vendedor == null)
            {
                return NotFound();
            }
            return View(vendedor);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid? id, [Bind("Id,Nome,Email")] VendedorViewModel vendedor)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            if (id != vendedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _vendedorService.Atualizar(_mapper.Map<Vendedor>(vendedor));

                return RedirectOrReturn(nameof(Index), nameof(Edit), vendedor);
            }

            return View(vendedor);
        }

        [Route("excluir/{id:guid}")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var vendedor = _mapper.Map<VendedorViewModel>(await _vendedorRepository.ObterPorId(id.Value));
            if (vendedor == null)
            {
                return NotFound();
            }
            return View(vendedor);
        }

        // POST: VendedoresController/Delete/5
        [HttpPost("excluir/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid? id, [Bind("Id,Nome,Email")] VendedorViewModel vendedor)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            await _vendedorRepository.Remover(id.Value);

            return RedirectOrReturn("Index", "Delete", vendedor);
        }
    }
}
