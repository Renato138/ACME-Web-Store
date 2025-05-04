using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Business.Interfaces.Services;
using Acme.Store.UI.Mvc.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Acme.Store.UI.Mvc.Controllers
{
    public class HomeController : MainController
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public HomeController(IProdutoService produtoService,
                              IMapper mapper,
                              INotificador notificador,
                              IAspNetUser aspNetUser,
                              ILogger<HomeController> logger) : base(notificador, logger, aspNetUser)
        {
            _mapper = mapper;
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = _mapper.Map<IEnumerable<ProdutoExibirViewModel>>(await _produtoService.ObterTodos())
                                .OrderBy(p => p.Nome);

            return View(produtos);
        }
    }
}
