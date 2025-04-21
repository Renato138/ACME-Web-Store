using Acme.Store.Abstractions.Interfaces.Services;
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

        Task<bool> AtualizarEmail(Guid userId, string novoEmail);

        Task<bool> AtualizarSenha(Guid userId, string novaSenha);

        Task<bool> Remover(Guid vendedorId);

        Task<string> Logar(Usuario usuario, TokenSettings tokenSettings, bool isPersistent, bool lockoutOnFailure);

        Task<bool> ValidarDisponibilidadeEmail(Guid userId, string email);

    }
}
