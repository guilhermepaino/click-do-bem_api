using SantaHelena.ClickDoBem.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SantaHelena.ClickDoBem.Domain.Core.Interfaces
{

    public interface IDomainServiceBase<TEntity> : IDisposable where TEntity : EntityBase<TEntity>
    {
        
        IEnumerable<TEntity> ObterTodos();
        TEntity ObterPorId(Guid id);
        IEnumerable<TEntity> Obter(Expression<Func<TEntity, bool>> predicado);

        TEntity Adicionar(TEntity entity);
        TEntity Atualizar(TEntity entity);
        void Excluir(Guid id);
        int SalvarMudancas { get; }
    }

}
