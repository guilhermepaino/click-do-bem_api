using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SantaHelena.ClickDoBem.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Objetos/Variáveis Locais

        protected readonly CdbContext _ctx;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de UnitOfWork
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public UnitOfWork(CdbContext ctx)
        {
            _ctx = ctx;
        }

        #endregion


        #region Métodos Públicos

        /// <summary>
        /// Efetivar alterações do contexto no banco de dados
        /// </summary>
        public void Efetivar()
        {
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Liberar recursos
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
        }

        #endregion

    }
}
