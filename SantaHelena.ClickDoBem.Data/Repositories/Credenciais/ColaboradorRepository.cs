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
    public class ColaboradorRepository : RepositorioBase<Colaborador>, IColaboradorRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public ColaboradorRepository(CdbContext ctx) : base(ctx) { }


        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public override Colaborador ObterPorId(Guid id)
        {

            string sql = @"SELECT * FROM Colaborador WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<Colaborador>
            (
                sql,
                new { pid = id }
            ).SingleOrDefault();

        }

        /// <summary>
        /// Obter registro pelo Cpf
        /// </summary>
        /// <param name="cpf">Cpf a ser buscado</param>
        public Colaborador ObterPorCpf(string cpf)
        {

            string sql = @"SELECT * FROM Colaborador WHERE Cpf = @pcpf";
            return _ctx.Database.GetDbConnection().Query<Colaborador>
            (
                sql,
                new { pcpf = cpf }
            ).SingleOrDefault();

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<Colaborador> ObterTodos()
        {
            string sql = @"SELECT * FROM Colaborador";
            return _ctx.Database.GetDbConnection().Query<Colaborador>(sql).ToList();
        }

        #endregion

    }
}
