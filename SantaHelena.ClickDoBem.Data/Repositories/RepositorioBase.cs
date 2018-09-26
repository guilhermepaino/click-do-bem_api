using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Core.Entities;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SantaHelena.ClickDoBem.Data.Repositories
{
    public abstract class RepositorioBase<TEntity> : IMySqlRepositoryBase<TEntity> where TEntity : EntityBase<TEntity>
    {

        protected CdbContext _ctx;
        protected DbSet<TEntity> DbSet;

        public RepositorioBase(CdbContext ctx)
        {
            _ctx = ctx;
            DbSet = _ctx.Set<TEntity>();
        }

        public abstract TEntity FindById(Guid id);

        public abstract IEnumerable<TEntity> GetAll();

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return DbSet.Update(entity).Entity;
        }

        public virtual void Delete(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
