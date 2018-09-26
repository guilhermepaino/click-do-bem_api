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
    /// Repositório da entidade Usuario
    /// </summary>
    public class UsuarioRepository : RepositorioBase<Usuario>, IUsuarioRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public UsuarioRepository(CdbContext ctx) : base(ctx) { }


        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public override Usuario ObterPorId(Guid id)
        {

            string sql = @"SELECT * FROM Usuario WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<Usuario>
            (
                sql,
                new { pid = id }
            ).SingleOrDefault();

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<Usuario> ObterTodos()
        {
            string sql = @"SELECT * FROM Usuario";
            return _ctx.Database.GetDbConnection().Query<Usuario>(sql).ToList();
        }

        #endregion

    }
}
