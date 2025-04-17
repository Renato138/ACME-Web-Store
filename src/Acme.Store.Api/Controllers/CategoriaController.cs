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
    [Route("api/categorias")]
    public class CategoriasController : MainController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository,
                                    ICategoriaService categoriaService,
                                    IMapper mapper,
                                    ILogger<CategoriasController> logger,
                                    INotificador notificador) : base(notificador, logger)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> ObterPorId(Guid id)
        {
            var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorId(id));

            if (categoria == null)
                return NotFound();

            return _mapper.Map<CategoriaViewModel>(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);

            await _categoriaService.Adicionar(categoria);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(categoria));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
            {
                NotificarErro("O id informado não é mesmo que foi informado na query.");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);

            await _categoriaService.Atualizar(categoria);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(categoria));

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (!await _categoriaRepository.Existe(id))
                return NotFound();

            await _categoriaRepository.Remover(id);

            return CustomResponse();
        }

    }
}
