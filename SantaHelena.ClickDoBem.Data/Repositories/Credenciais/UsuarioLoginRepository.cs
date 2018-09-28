using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Data.Repositories.Credenciais
{

    /// <summary>
    /// Repositório da entidade UsuarioLogin
    /// </summary>
    public class UsuarioLoginRepository : RepositorioBase<UsuarioLogin>, IUsuarioLoginRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public UsuarioLoginRepository(CdbContext ctx) : base(ctx) { }


        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public override UsuarioLogin ObterPorId(Guid id)
        {

            string sql = @"SELECT * FROM UsuarioLogin WHERE UsuarioId = @pid";
            return _ctx.Database.GetDbConnection().Query<UsuarioLogin>
            (
                sql,
                new { pid = id }
            ).SingleOrDefault();

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<UsuarioLogin> ObterTodos()
        {
            string sql = @"SELECT * FROM UsuarioLogin";
            return _ctx.Database.GetDbConnection().Query<UsuarioLogin>(sql).ToList();
        }

        #endregion

    }
}
