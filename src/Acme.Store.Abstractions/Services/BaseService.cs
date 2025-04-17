
using Acme.Store.Abstractions.Interfaces;
using Acme.Store.Abstractions.Interfaces.Services;
using Acme.Store.Abstractions.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Store.Abstractions.Services
{
    public abstract class BaseService<TValidator, TEntity> : IBaseService<TValidator, TEntity> where TValidator : AbstractValidator<TEntity> where TEntity : Entity
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        public bool ExecutarValidacao(TValidator validator, TEntity entidade)
        {
            ValidationResult validationResult = validator.Validate(entidade);

            if (validationResult.IsValid) 
                return true;

            Notificar(validationResult);

            return false;
        }

        public void Notificar(IEnumerable<string> mensagens)
        {
            foreach (var mensagem in mensagens)
            {
                Notificar(mensagem);
            }
        }

        public void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        public void Notificar(string mensagem)
        {
            _notificador.Handle(mensagem);
        }

        public abstract void Dispose();

    }
}
