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
    public class TipoItemRepository : RepositorioBase<TipoItem>, ITipoItemRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public TipoItemRepository(CdbContext ctx) : base(ctx) { }

        public override TipoItem ObterPorId(Guid id)
        {
            string sql = @"SELECT * FROM TipoItem WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<TipoItem>(sql, new { pid = id }).SingleOrDefault();
        }

        public override IEnumerable<TipoItem> ObterTodos()
        {
            string sql = @"SELECT * FROM TipoItem";
            return _ctx.Database.GetDbConnection().Query<TipoItem>(sql).ToList();
        }

        public TipoItem ObterPorDescricao(string descricao)
        {
            string sql = @"SELECT * FROM TipoItem WHERE Descricao = @pdescricao";
            return _ctx.Database.GetDbConnection().Query<TipoItem>(sql, new { pdescricao = descricao }).SingleOrDefault();
        }

        public IEnumerable<TipoItem> ObterPorSemelhanca(string descricao)
        {
            string sql = @"SELECT * FROM TipoItem WHERE Descricao LIKE @pdescricao";
            return _ctx.Database.GetDbConnection().Query<TipoItem>(sql, new { pdescricao = $"%{descricao}%" }).ToList();
        }


        #endregion

    }
}
