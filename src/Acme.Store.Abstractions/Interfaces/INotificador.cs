using Acme.Store.Abstractions.Notitications;
using System.Collections.Generic;

namespace Acme.Store.Abstractions.Interfaces
{
    public interface INotificador
    {
        event NotificacaoEventHandler NotificacaoAdicionada;

        bool TemNotificacao();

        IEnumerable<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);

        void Handle(string mensagem);
    }
}
