using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Auth.Models.Info
{
    public class UsuarioModelInfo
    {
        public const int NomeMinLength = 3;
        public const int NomeMaxLength = 150;
        public const int SenhaMinLength = 8;
        public const int SenhaMaxLength = 150;
        public const int EmailMinLength = 3;
        public const int EmailMaxLength = 250;
    }

}
