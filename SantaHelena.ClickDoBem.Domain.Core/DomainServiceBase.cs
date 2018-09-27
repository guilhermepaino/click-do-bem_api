using SantaHelena.ClickDoBem.Domain.Core.Entities;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SantaHelena.ClickDoBem.Domain.Core
{

    public abstract class DomainServiceBase<TEntity, TRepository> : IDomainServiceBase<TEntity> where TEntity : EntityBase<TEntity> where TRepository : IMySqlRepositoryBase<TEntity>
    {

        protected TRepository _repository;

        public DomainServiceBase(TRepository repository)
        {
            _repository = repository;
        }

        public virtual TEntity Adicionar(TEntity entity)
        {
            if (!entity.EstaValido())
                return entity;

            return _repository.Adicionar(entity);
        }

        public virtual TEntity Atualizar(TEntity entity)
        {
            if (!entity.EstaValido())
                return entity;

            return _repository.Atualizar(entity);
        }

        public virtual void Excluir(Guid id)
        {
            _repository.Excluir(id);
        }
        public int SalvarMudancas => _repository.SalvarMudancas();


        public IEnumerable<TEntity> ObterTodos() => _repository.ObterTodos();

        public TEntity ObterPorId(Guid id) => _repository.ObterPorId(id);

        public IEnumerable<TEntity> Obter(Expression<Func<TEntity, bool>> predicado) => _repository.Obter(predicado);
        

        public virtual void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }

    }

}
