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
    public class TipoMatchRepository : RepositorioBase<TipoMatch>, ITipoMatchRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public TipoMatchRepository(CdbContext ctx) : base(ctx) { }

        public override TipoMatch ObterPorId(Guid id)
        {
            string sql = @"SELECT * FROM TipoMatch WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<TipoMatch>(sql, new { pid = id }).SingleOrDefault();
        }

        public override IEnumerable<TipoMatch> ObterTodos()
        {
            string sql = @"SELECT * FROM TipoMatch";
            return _ctx.Database.GetDbConnection().Query<TipoMatch>(sql).ToList();
        }

        public TipoMatch ObterPorDescricao(string descricao)
        {
            string sql = @"SELECT * FROM TipoMatch WHERE Descricao = @pdescricao";
            return _ctx.Database.GetDbConnection().Query<TipoMatch>(sql, new { pdescricao = descricao }).SingleOrDefault();
        }

        public IEnumerable<TipoMatch> ObterPorSemelhanca(string descricao)
        {
            string sql = @"SELECT * FROM TipoMatch WHERE Descricao LIKE @pdescricao";
            return _ctx.Database.GetDbConnection().Query<TipoMatch>(sql, new { pdescricao = $"%{descricao}%" }).ToList();
        }


        #endregion

    }
}
