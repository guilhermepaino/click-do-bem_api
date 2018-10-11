using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>
        /// Obter registros pela lista de id de itens
        /// </summary>
        /// <param name="ids">Lista de ids</param>
        public IEnumerable<ItemImagem> ObterPorLista(List<Guid> ids)
        {

            if (ids == null || ids.Count().Equals(0))
                return null;

            StringBuilder sbFiltro = new StringBuilder();
            ids.ForEach(id => sbFiltro.Append($"'{id.ToString()}',"));

            string sql = $@"SELECT * FROM ItemImagem WHERE ItemId IN ({sbFiltro.ToString().Substring(0, (sbFiltro.Length - 1))})";
            IEnumerable<ItemImagem> imagens = _ctx.Database.GetDbConnection().Query<ItemImagem>(sql).ToList();
            return imagens;

        }


        #endregion

    }
}
