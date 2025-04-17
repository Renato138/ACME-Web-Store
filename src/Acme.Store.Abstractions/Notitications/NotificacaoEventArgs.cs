using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Abstractions.Notitications
{
    public delegate void NotificacaoEventHandler(object sender, NotificacaoEventArgs e);

    public class NotificacaoEventArgs : EventArgs
    {
        public Notificacao Notificacao { get; set; }
        public NotificacaoEventArgs(Notificacao notificacao)
        {
            Notificacao = notificacao;
        }
    }
}
