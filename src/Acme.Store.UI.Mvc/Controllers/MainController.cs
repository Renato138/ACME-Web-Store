using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Acme.Store.Business.Interfaces;
using Acme.Store.Business.Models;
using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Notitications;

namespace Acme.Store.UI.Mvc.Controllers
{
    public abstract class MainController : Controller
    {
        protected INotificador _notificador;
        protected ILogger<MainController> _logger;

        public MainController(INotificador notificador, ILogger<MainController> logger)
        {
            _logger = logger;
            _notificador = notificador;
            _notificador.NotificacaoAdicionada += OnNotificacaoAdicionada;
        }

        private void OnNotificacaoAdicionada(object sender, NotificacaoEventArgs e)
        {
            ModelState.AddModelError(string.Empty, e.Notificacao.Mensagem);
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult RedirectOrReturn(string sucessViewName, string errorViewName, object? model = null)
        {
            if (OperacaoValida())
            {
                return RedirectToAction(sucessViewName);
            }
            else
            {
                return View(errorViewName, model); ;
            }
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
