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
    /// Repositório da entidade Colaborador
    /// </summary>
    public class UsuarioDadosRepository : RepositorioBase<UsuarioDados>, IUsuarioDadosRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public UsuarioDadosRepository(CdbContext ctx) : base(ctx) { }


        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public override UsuarioDados ObterPorId(Guid id)
        {

            string sql = @"SELECT * FROM UsuarioDados WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<UsuarioDados>
            (
                sql,
                new { pid = id }
            ).SingleOrDefault();

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<UsuarioDados> ObterTodos()
        {
            string sql = @"SELECT * FROM UsuarioDados";
            return _ctx.Database.GetDbConnection().Query<UsuarioDados>(sql).ToList();
        }

        #endregion

    }
}
