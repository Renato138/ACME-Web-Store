using Acme.Store.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Store.Abstractions.Notitications
{
    public class Notificador : INotificador
    {
        private readonly List<Notificacao> _notificacoes;

        public event NotificacaoEventHandler NotificacaoAdicionada;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }
        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
            NotificacaoAdicionada?.Invoke(this, new NotificacaoEventArgs(notificacao));
        }

        public void Handle(string mensagem)
        {
            var notificacao = new Notificacao(mensagem);
            _notificacoes.Add(notificacao);
            NotificacaoAdicionada?.Invoke(this, new NotificacaoEventArgs(notificacao));
        }

        public IEnumerable<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
