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
    public class ItemMatchRepository : RepositorioBase<ItemMatch>, IItemMatchRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public ItemMatchRepository(CdbContext ctx) : base(ctx) { }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo Id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public override ItemMatch ObterPorId(Guid id)
        {
            string sql = @"SELECT * FROM ItemMatch WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<ItemMatch>(sql, new { pid = id }).SingleOrDefault();
        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<ItemMatch> ObterTodos()
        {
            string sql = @"SELECT * FROM ItemMatch";
            return _ctx.Database.GetDbConnection().Query<ItemMatch>(sql).ToList();
        }

        /// <summary>
        /// Localizar um item de match pelos ids dos itens
        /// </summary>
        /// <param name="doacaoId">Id do item de doação</param>
        /// <param name="necessidadeId">Id do item de necessidade</param>
        public ItemMatch BuscarPorMatch(Guid doacaoId, Guid necessidadeId)
        {
            string sql = @"SELECT * FROM ItemMatch WHERE NecessidadeId = @pNecessidadeId AND DoacaoId = @pDoacaoId";
            return _ctx.Database.GetDbConnection().Query<ItemMatch>(sql, new { pNecessidadeId = necessidadeId, pDoacaoId = doacaoId }).FirstOrDefault();
        }

        #endregion


    }
}
