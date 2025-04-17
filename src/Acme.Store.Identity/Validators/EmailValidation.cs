using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Auth.Validators
{
    public class EmailValidation
    {
        public string Email { get; }
        public EmailValidation(string email)
        {
            Email = email;
        }
    }
}
