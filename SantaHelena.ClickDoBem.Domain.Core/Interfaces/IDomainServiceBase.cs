using SantaHelena.ClickDoBem.Domain.Core.Entities;
using System;

namespace SantaHelena.ClickDoBem.Domain.Core.Interfaces
{

    public interface IDomainServiceBase<TEntity> : IDisposable where TEntity : EntityBase<TEntity>
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(Guid id);
    }

}
