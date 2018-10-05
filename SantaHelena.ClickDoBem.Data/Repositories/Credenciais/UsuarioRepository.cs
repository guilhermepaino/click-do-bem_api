using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
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

        protected void CarregarRelacoesUsuario(IEnumerable<Usuario> usuarios)
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

                // UsuariosPerfil
                sql = @"SELECT * FROM UsuarioPerfil WHERE UsuarioId = @pid";
                usuario.Perfis = _ctx.Database.GetDbConnection().Query<UsuarioPerfil>(sql, new { pid = usuario.Id }).ToList();

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

            string sql = $@"SELECT u.* FROM Usuario u INNER JOIN UsuarioLogin us ON u.Id = us.UsuarioId WHERE us.Login = @pusuario AND us.Senha = @psenha";
            Usuario usuario = _ctx.Database.GetDbConnection().Query<Usuario>(sql,  new { pusuario = login, psenha = senha }).FirstOrDefault();
            CarregarRelacoesUsuario(usuario);
            return usuario;

        }

        /// <summary>
        /// Buscar pelo número do documento (cpf/cnpj)
        /// </summary>
        /// <param name="documento">Número do documento cpf/cnpj</param>
        public Usuario ObterPorDocumento(string documento)
        {
            string sql = $@"SELECT * FROM Usuario u WHERE u.CpfCnpj = @pdocumento";
            Usuario usuario = _ctx.Database.GetDbConnection().Query<Usuario>(sql, new { pdocumento = documento }).FirstOrDefault();
            CarregarRelacoesUsuario(usuario);
            return usuario;
        }

        /// <summary>
        /// Buscar usuário
        /// </summary>
        /// <param name="perfil">Perfil de filtro</param>
        public IEnumerable<Usuario> ObterPorPerfil(string perfil)
        {
            string sql = $@"SELECT u.*
                            FROM Usuario u 
                            INNER JOIN UsuarioPerfil up ON u.Id = up.UsuarioId
                            WHERE up.Perfil = @pperfil";
            IEnumerable<Usuario> usuarios = _ctx.Database.GetDbConnection().Query<Usuario>(sql, new { pperfil = perfil }).ToList();
            CarregarRelacoesUsuario(usuarios);
            return usuarios;
        }

        /// <summary>
        /// Verifica a situação do documento
        /// </summary>
        /// <param name="documento">Documento a ser verificado</param>
        /// <param name="situacao">Varíavel de saída de situação do documento</param>
        /// <param name="cadastrado">Varíavel de saída de flag de situaçaõ de cadastro completo</param>
        public void VerificarSituacaoDocumento(string documento, out string situacao, out bool cadastrado)
        {
            situacao = "inexistente";
            cadastrado = false;

            string sql = "SELECT * FROM DocumentoHabilitado WHERE CpfCnpj = @pdoc";
            DocumentoHabilitado doc = _ctx.Database.GetDbConnection().Query<DocumentoHabilitado>(sql, new { pdoc = documento }).FirstOrDefault();
            if (doc != null)
            { 
                situacao = doc.Ativo ? "ativo" : "inativo";
                sql = "SELECT * FROM Usuario u INNER JOIN UsuarioDados ud ON u.Id = ud.UsuarioId WHERE u.CpfCnpj = @pdoc";
                Usuario usuario = _ctx.Database.GetDbConnection().Query<Usuario>(sql, new { pdoc = documento }).FirstOrDefault();
                cadastrado = (usuario != null);
            }
        }

        #endregion

    }
}
