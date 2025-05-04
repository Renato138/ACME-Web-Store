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
using Acme.Store.Business.Constants;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Acme.Store.UI.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
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
                                    ILogger<MainController> logger,
                                    IAspNetUser aspNetUser) : base(notificador, logger, aspNetUser)
        {
            _vendedorService = vendedorService;
            _vendedorRepository = vendedorRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [Route("criar-novo")]
        public async Task<ActionResult> Create()
        {
            var vendedor = new VendedorIncluirViewModel();

            //var senha = GeneratePassword();
            //vendedor.Senha = senha;
            //vendedor.ConfirmeSenha = senha;

            return View(vendedor);
        }

        [AllowAnonymous]
        [HttpPost("criar-novo")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Nome,Email,Senha,ConfirmeSenha")] VendedorIncluirViewModel vendedor)
        {
            var isAdmin = _aspNetUser.IsAuthenticated && _aspNetUser.IsInRole(Roles.Admin);

            if (ModelState.IsValid)
            {
                if (vendedor == null)
                {
                    return NotFound();
                }

                await _vendedorService.Adicionar(_mapper.Map<Vendedor>(vendedor), vendedor.Senha, true, ! isAdmin);

                if (isAdmin)
                    return RedirectOrReturn(nameof(Index), nameof(Create), vendedor);
                else
                {
                    if (OperacaoValida())
                        return RedirectToAction("Index", "Home");
                    else
                        return View(vendedor);
                }
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

        [HttpPost("excluir/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid? id, [Bind("Id,Nome,Email")] VendedorViewModel vendedor)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            await _vendedorService.Remover(id.Value);

            if (! OperacaoValida())
            {
                vendedor = _mapper.Map<VendedorViewModel>(await _vendedorRepository.ObterPorId(id.Value));
                if (vendedor == null)
                {
                    return NotFound();
                }
                return View(vendedor);
            }

            return Redirect("Index");
        }

        private static string GeneratePassword()
        {
            // Parte 1
            var random = new Random();

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            var part1 = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());

            // Parte 2
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var part2 = chars[random.Next(chars.Length)].ToString();

            // Parte 3
            chars = "abcdefghijklmnopqrstuvwxyz";
            var part3 = chars[random.Next(chars.Length) ].ToString();

            // Parte 4
            chars = "0123456789";
            var part4 = chars[random.Next(chars.Length)].ToString();

            // Parte 5
            chars = ".!?*";
            var part5 = chars[random.Next(chars.Length)].ToString();

            return $"{part1}{part2}{part3}{part4}{part5}";
        }

    }
}
