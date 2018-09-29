using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Data;
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

        #region Métodos Locais

        protected void CarregarRelacoesUsuario(IList<Usuario> usuarios)
        {
            foreach (Usuario u in usuarios)
            {
                CarregarRelacoesUsuario(u);
            }
        }

        protected void CarregarRelacoesUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                string sql;

                // UsuarioLogin
                sql = @"SELECT * FROM UsuarioLogin WHERE UsuarioId = @pid";
                usuario.UsuarioLogin = _ctx.Database.GetDbConnection().Query<UsuarioLogin>(sql, new { pid = usuario.Id }).SingleOrDefault();

                // UsuarioDados
                sql = @"SELECT * FROM UsuarioDados WHERE UsuarioId = @pid";
                usuario.UsuarioDados = _ctx.Database.GetDbConnection().Query<UsuarioDados>(sql, new { pid = usuario.Id }).SingleOrDefault();

            }
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public override Usuario ObterPorId(Guid id)
        {

            string sql = null;

            // Usuario
            sql = @"SELECT * FROM Usuario WHERE Id = @pid";
            Usuario usuario = _ctx.Database.GetDbConnection().Query<Usuario>(sql, new { pid = id }).SingleOrDefault();
            CarregarRelacoesUsuario(usuario);
            return usuario;

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<Usuario> ObterTodos()
        {
            string sql = @"SELECT * FROM Usuario";
            List<Usuario> usuarios = _ctx.Database.GetDbConnection().Query<Usuario>(sql).ToList();

            CarregarRelacoesUsuario(usuarios);

            return usuarios;
        }

        /// <summary>
        /// Buscar usuário
        /// </summary>
        /// <param name="login">Nome do usuário</param>
        /// <param name="senha">Senha (Hash Md5) do usuário</param>
        public Usuario ObterPorLogin(string login, string senha)
        {

            string sql = null;

            // Usuario
            sql = $@"SELECT u.* FROM Usuario u INNER JOIN UsuarioLogin us ON u.Id = us.UsuarioId WHERE u.Nome = @pusuario AND us.Senha = @psenha";
            Usuario usuario = _ctx.Database.GetDbConnection().Query<Usuario>(sql,  new { pusuario = login, psenha = senha }).FirstOrDefault();
            CarregarRelacoesUsuario(usuario);
            return usuario;

        }

        #endregion

    }
}
