using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Data.Repositories.Cadastros
{
    public class ValorFaixaRepository : RepositorioBase<ValorFaixa>, IValorFaixaRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public ValorFaixaRepository(CdbContext ctx) : base(ctx) { }

        public override ValorFaixa ObterPorId(Guid id)
        {
            string sql = null;

            sql = @"SELECT * FROM ValorFaixa WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<ValorFaixa>(sql, new { pid = id }).SingleOrDefault();
        }

        public override IEnumerable<ValorFaixa> ObterTodos()
        {
            string sql = @"SELECT * FROM ValorFaixa ORDER BY ValorInicial";
            return _ctx.Database.GetDbConnection().Query<ValorFaixa>(sql).ToList();
        }

        #endregion

    }
}
