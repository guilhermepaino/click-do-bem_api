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

        /// <summary>
        /// Buscar usuário
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="senha">Senha (Hash Md5) do usuário</param>
        public Usuario ObterPorUsuarioSenha(string usuario, string senha)
        {
            
            string sql = $@"SELECT u.*
                            FROM Usuario u
                            INNER JOIN UsuarioSenha us ON u.Id = us.UsuarioId
                            WHERE u.Nome = @pusuario AND us.Senha = @psenha";

            return _ctx.Database
                .GetDbConnection()
                .Query<Usuario>
                (
                    sql, 
                    new { pusuario = usuario, psenha = senha }
                ).FirstOrDefault();

        }

        #endregion

    }
}
