using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Api.ViewModels;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Auth.Models;
using Acme.Store.Auth.Token;
using Acme.Store.Business.Interfaces.Repositories;
using Acme.Store.Business.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Acme.Store.Api.Controllers
{
    public class AuthController : MainController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly TokenSettings _tokenSettings;

        public AuthController(IUsuarioService usuarioService,
                              IOptions<TokenSettings> tokenSettings,
                              IMapper mapper,
                              ILogger<VendedoresController> logger,
                              INotificador notificador,
                              IAspNetUser aspNetUser) : base(notificador, logger, aspNetUser)
        {
            _usuarioService = usuarioService;
            _tokenSettings = tokenSettings.Value;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var usuario = _mapper.Map<Usuario>(loginViewModel);

            var token = await _usuarioService.LogarApi(usuario, _tokenSettings, false, true);
            
            if (string.IsNullOrWhiteSpace(token))
            {
                return CustomResponse(loginViewModel);
            }

            return CustomResponse(token);
        }

    }
}
