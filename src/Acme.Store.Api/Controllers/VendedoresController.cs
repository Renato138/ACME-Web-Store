using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Api.ViewModels;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Auth.Models;
using Acme.Store.Business.Interfaces;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Acme.Store.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/vendedores")]
    public class VendedoresController : MainController
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IVendedorService _vendedorService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public VendedoresController(IVendedorRepository vendedorRepository,
                                    IVendedorService vendedorService,
                                    IUsuarioService usuarioService,
                                    IMapper mapper,
                                    ILogger<VendedoresController> logger,
                                    INotificador notificador,
                                    IAspNetUser aspNetUser) : base(notificador, logger, aspNetUser)
        {
            _vendedorRepository = vendedorRepository;
            _vendedorService = vendedorService;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<VendedorViewModel>> ObterTodos()
        {
            var list = await _vendedorRepository.ObterTodos();
            return _mapper.Map<IEnumerable<VendedorViewModel>>(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<VendedorViewModel>> ObterPorId(Guid id)
        {
            var vendedor = _mapper.Map<VendedorViewModel>(await _vendedorRepository.ObterPorId(id));

            if (vendedor == null)
                return NotFound();

            return _mapper.Map<VendedorViewModel>(vendedor);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(VendedorIncluirViewModel vendedorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var vendedor = _mapper.Map<Vendedor>(vendedorViewModel);

            // Esta conta já é criada como confirmada, pois é criada por um usuário Admin
            // e será associada a um vendedor.
            await _vendedorService.Adicionar(vendedor, vendedorViewModel.Senha, true);

            return CustomResponse(_mapper.Map<VendedorViewModel>(vendedor));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, VendedorViewModel vendedorViewModel)
        {
            if (id != vendedorViewModel.Id)
            {
                NotificarErro("O id informado não é mesmo que foi informado na query.");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var vendedor = _mapper.Map<Vendedor>(vendedorViewModel);

            await _vendedorService.Atualizar(vendedor);

            return CustomResponse(_mapper.Map<VendedorViewModel>(vendedor));

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (!await _vendedorRepository.Existe(id))
            {
                return NotFound();
            }

            await _vendedorRepository.Remover(id);

            return CustomResponse();
        }

    }
}
