﻿using Acme.Store.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Auth.Models
{
    public class Usuario : Entity
    {
        //public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
