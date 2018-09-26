using SantaHelena.ClickDoBem.Domain.Core.Entities;
using System;

namespace SantaHelena.ClickDoBem.Domain.Core.Interfaces
{

    public interface IDomainServiceBase<TEntity> : IDisposable where TEntity : EntityBase<TEntity>
    {
        TEntity Adicionar(TEntity entity);
        TEntity Atualizar(TEntity entity);
        void Excluir(Guid id);
    }

}
