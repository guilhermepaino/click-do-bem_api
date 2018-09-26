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

    /// <summary>
    /// Classe abstrata do repositório base
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidade</typeparam>
    public abstract class RepositorioBase<TEntity> : IMySqlRepositoryBase<TEntity> where TEntity : EntityBase<TEntity>
    {

        #region Objetos/Variáveis locais

        protected CdbContext _ctx;
        protected DbSet<TEntity> DbSet;

        #endregion

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx"></param>
        public RepositorioBase(CdbContext ctx)
        {
            _ctx = ctx;
            DbSet = _ctx.Set<TEntity>();
        }

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public abstract TEntity ObterPorId(Guid id);

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public abstract IEnumerable<TEntity> ObterTodos();

        /// <summary>
        /// Obter registros através de filtro personalizado
        /// </summary>
        /// <param name="predicate">Predicato de filtro</param>
        public virtual IEnumerable<TEntity> Obter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        /// <summary>
        /// Adicionar registro no contexto
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada</param>
        public virtual TEntity Adicionar(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        /// <summary>
        /// Atualizar registro no contexto
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        public virtual TEntity Atualizar(TEntity entity)
        {
            return DbSet.Update(entity).Entity;
        }

        /// <summary>
        /// Excluir registro do contexto
        /// </summary>
        /// <param name="id">Id do registro a ser excluído</param>
        public virtual void Excluir(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        /// <summary>
        /// Salvar as mudanças realizadas no contexto na base de dados
        /// </summary>
        public int SalvarMudancas()
        {
            return _ctx.SaveChanges();
        }

        /// <summary>
        /// Liberar recursos
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
