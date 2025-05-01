using FluentValidation;
using FluentValidation.Results;
using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Models;
using Acme.Store.Abstractions.Services;
using Acme.Store.Auth.Interfaces;
using Acme.Store.Auth.Models;
using Acme.Store.Auth.Token;
using Acme.Store.Auth.Validators;
using Acme.Store.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Acme.Store.Auth.Context;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Acme.Store.Auth.Services
{
    public class UsuarioService : BaseService<UsuarioValidator, Usuario>, IUsuarioService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenSettings _tokenSettings;
        private readonly AcmeIdentityDbContext _context;

        public UsuarioService(INotificador notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<TokenSettings> tokenSettings) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<IdentityUser> Adicionar(Usuario usuario, bool emailConfirmed = false)
        {
            if (!ExecutarValidacao(new UsuarioValidator(), usuario))
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(usuario.Email);
            if (user != null) 
            {
                Notificar("Já existe um usuário cadastrado com este email.");
                return null;
            }

            user = new IdentityUser()
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = emailConfirmed
            };

            var result = await _userManager.CreateAsync(user, usuario.Senha);
            if (! result.Succeeded)
            {
                Notificar(result.Errors.Select(e => e.Description));
                return null;
            }

            return user;
        }

        public async Task<bool> AtualizarEmail(Guid userId, string novoEmail)
        {
            if (!Validar<EmailValidation>(new EmailValidator(), new EmailValidation(novoEmail)))
            {
                return false;
            }

            var user = await _userManager.FindByEmailAsync(novoEmail);
            if (user != null && Guid.Parse(user.Id) != userId)
            {
                Notificar("Já existe um usuário cadastrado com este email.");
                return false;
            }

            user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                Notificar("Usuário do Identity não localizado.");
                return false;
            }

            var result = await _userManager.SetEmailAsync(user, novoEmail);
            if (!result.Succeeded)
            {
                Notificar(result.Errors.Select(e => e.Description));
                return false;
            }

            result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                Notificar(result.Errors.Select(e => e.Description));
                return false;
            }

            return true;
        }

        public async Task<string> LogarApi(Usuario usuario, TokenSettings tokenSettings, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, isPersistent, lockoutOnFailure);

            if (result.IsLockedOut)
            {
                Notificar("Usuário temporariamente bloqueado por tentativas inválidas");
                return null;
            }

            if (!result.Succeeded)
            {
                Notificar("Usuário ou Senha incorretos");
            }
            var token = GerarJwt(tokenSettings);

            return token;
        }

        public async Task<SignInResult> LogarSite(Usuario usuario, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, isPersistent, lockoutOnFailure);

            if (result.IsLockedOut)
            {
                Notificar("Usuário temporariamente bloqueado por tentativas inválidas");
                return null;
            }

            if (!result.Succeeded)
            {
                Notificar("Usuário ou Senha incorretos");
            }
            return result;
        }

        public async Task<bool> Remover(Guid id)
        {
            var userId = id.ToString();
            var user = await _userManager.FindByIdAsync(userId);

            user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                Notificar("Usuário do Identity não localizado.");
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                Notificar(result.Errors.Select(e => e.Description));
                return false;
            }

            return true;
        }

        public async Task<IdentityUser> ObterPorId(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task<IdentityUser> ObterPorEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }   

        public async Task<bool> ValidarDisponibilidadeEmail(Guid userId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && Guid.Parse(user.Id) != userId)
            {
                Notificar("Já existe um usuário cadastrado com este email.");
                return false;
            }
            return true;
        }

        public bool Validar<T>(AbstractValidator<T> validator, T entidade)
        {
            ValidationResult validationResult = validator.Validate(entidade);

            if (validationResult.IsValid)
                return true;

            Notificar(validationResult);

            return false;
        }

        public Task<bool> AtualizarSenha(Guid userId, string novaSenha)
        {
            throw new NotImplementedException();
        }

        private string GerarJwt(TokenSettings tokenSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenSettings.Emissor,
                Audience = tokenSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(tokenSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

        public override void Dispose()
        {
        }
    }
}
