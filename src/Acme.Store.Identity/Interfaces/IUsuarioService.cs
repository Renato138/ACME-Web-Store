﻿using Acme.Store.Abstractions.Interfaces.Services;
using Acme.Store.Auth.Models;
using Acme.Store.Auth.Token;
using Acme.Store.Auth.Validators;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Auth.Interfaces
{
    public interface IUsuarioService : IBaseService<UsuarioValidator, Usuario>
    {
        Task<IdentityUser> ObterPorId(Guid id);

        Task<IdentityUser> ObterPorEmail(string email);

        Task<IdentityUser> Adicionar(Usuario usuario, bool emailConfirmed = false);

        Task<bool> Atualizar(Guid userId, string novoEmail);

        Task<bool> Remover(Guid vendedorId);

        Task<string> LogarApi(Usuario usuario, TokenSettings tokenSettings, bool isPersistent, bool lockoutOnFailure);

        //Task<string> LogarSite(Usuario usuario, bool isPersistent, bool lockoutOnFailure);

        Task<bool> ValidarDisponibilidadeEmail(Guid userId, string email);

        //Task<bool> ValidarDisponibilidadeNome(Guid userId, string nome);

    }
}
