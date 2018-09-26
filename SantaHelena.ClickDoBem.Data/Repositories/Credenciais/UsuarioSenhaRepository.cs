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
    /// Repositório da entidade UsuarioSenha
    /// </summary>
    public class UsuarioSenhaRepository : RepositorioBase<UsuarioSenha>, IUsuarioSenhaRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public UsuarioSenhaRepository(CdbContext ctx) : base(ctx) { }


        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public override UsuarioSenha ObterPorId(Guid id)
        {

            string sql = @"SELECT * FROM UsuarioSenha WHERE UsuarioId = @pid";
            return _ctx.Database.GetDbConnection().Query<UsuarioSenha>
            (
                sql,
                new { pid = id }
            ).SingleOrDefault();

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<UsuarioSenha> ObterTodos()
        {
            string sql = @"SELECT * FROM UsuarioSenha";
            return _ctx.Database.GetDbConnection().Query<UsuarioSenha>(sql).ToList();
        }

        #endregion

    }
}
