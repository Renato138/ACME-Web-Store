using System;
using System.Linq;
using System.Text;

namespace Acme.Store.Business.Notitications
{
    public class Notificacao
    {
        public string Mensagem { get; }

        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}
