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
    /// Repositório da entidade UsuarioPerfil
    /// </summary>
    public class UsuarioPerfilRepository : RepositorioBase<UsuarioPerfil>, IUsuarioPerfilRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public UsuarioPerfilRepository(CdbContext ctx) : base(ctx) { }


        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        public override UsuarioPerfil ObterPorId(Guid id)
        {

            string sql = @"SELECT * FROM UsuarioPerfil WHERE UsuarioId = @pid";
            return _ctx.Database.GetDbConnection().Query<UsuarioPerfil>
            (
                sql,
                new { pid = id }
            ).SingleOrDefault();

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<UsuarioPerfil> ObterTodos()
        {
            string sql = @"SELECT * FROM UsuarioPerfil";
            return _ctx.Database.GetDbConnection().Query<UsuarioPerfil>(sql).ToList();
        }

        #endregion

    }
}
