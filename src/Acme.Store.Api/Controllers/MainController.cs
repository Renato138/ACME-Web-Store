using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Notitications;
using System.Collections.Generic;
using Acme.Store.Auth.Interfaces;

namespace Acme.Store.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected INotificador _notificador;
        protected ILogger<MainController> _logger;
        protected IAspNetUser _aspNetUser;


        protected bool UsuarioAutenticado{ get; }
        protected Guid UsuarioId { get; }


        public MainController(INotificador notificador, 
                              ILogger<MainController> logger,
                              IAspNetUser spNetUser)
        {
            _notificador = notificador;
            _logger = logger;
            _aspNetUser = spNetUser;

            UsuarioAutenticado = _aspNetUser?.IsAuthenticated() ?? false;
            UsuarioId = UsuarioAutenticado ? _aspNetUser.GetUserId() : Guid.Empty;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
                });
            }
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                NotificarErroModelInvalida(modelState);
            }

            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var mensagem = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(mensagem);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}
