using SantaHelena.ClickDoBem.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SantaHelena.ClickDoBem.Domain.Core.Interfaces
{
    public interface IMySqlRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase<TEntity>
    {

        TEntity Adicionar(TEntity entidade);
        IEnumerable<TEntity> Obter(Expression<Func<TEntity, bool>> predicado);
        TEntity ObterPorId(Guid id);
        IEnumerable<TEntity> ObterTodos();
        void Excluir(Guid id);
        TEntity Atualizar(TEntity entity);
        int SalvarMudancas();

    }
}
