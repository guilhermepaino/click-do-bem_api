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
    public class ItemImagemRepository : RepositorioBase<ItemImagem>, IItemImagemRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public ItemImagemRepository(CdbContext ctx) : base(ctx) { }

        public override ItemImagem ObterPorId(Guid id)
        {
            string sql = @"SELECT * FROM ItemImagem WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<ItemImagem>(sql, new { pid = id }).SingleOrDefault();
        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<ItemImagem> ObterTodos()
        {
            string sql = @"SELECT * FROM ItemImagem";
            return _ctx.Database.GetDbConnection().Query<ItemImagem>(sql).ToList();
        }

        /// <summary>
        /// Obter registros pelo Item
        /// </summary>
        /// <param name="itemId">Id do item</param>
        public IEnumerable<ItemImagem> ObterPorItem(Guid itemId)
        {
            string sql = @"SELECT * FROM ItemImagem WHERE ItemId = @pitemid";
            return _ctx.Database.GetDbConnection().Query<ItemImagem>(sql, new { pitemid = itemId }).ToList();
        }


        #endregion

    }
}
