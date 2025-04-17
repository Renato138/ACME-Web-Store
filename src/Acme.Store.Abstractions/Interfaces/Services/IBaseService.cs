using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Store.Abstractions.Models;

namespace Acme.Store.Abstractions.Interfaces.Services
{
    public interface IBaseService<TValidator, TEntity> : IDisposable where TValidator : AbstractValidator<TEntity> where TEntity : Entity
    {
        void Notificar(ValidationResult validationResult);

        void Notificar(string mensagem);

        bool ExecutarValidacao(TValidator validacao, TEntity entidade);

    }
}
