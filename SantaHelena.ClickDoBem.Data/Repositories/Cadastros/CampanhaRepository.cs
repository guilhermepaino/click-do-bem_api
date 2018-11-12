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
    public class CampanhaRepository : RepositorioBase<Campanha>, ICampanhaRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public CampanhaRepository(CdbContext ctx) : base(ctx) { }

        public override Campanha ObterPorId(Guid id)
        {
            string sql = null;

            sql = @"SELECT * FROM Campanha WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<Campanha>(sql, new { pid = id }).SingleOrDefault();
        }

        public override IEnumerable<Campanha> ObterTodos()
        {
            string sql = @"SELECT * FROM Campanha";
            return _ctx.Database.GetDbConnection().Query<Campanha>(sql).ToList();
        }

        public Campanha ObterPorDescricao(string descricao)
        {
            string sql = @"SELECT * FROM Campanha WHERE Descricao = @pdescricao";
            return _ctx.Database.GetDbConnection().Query<Campanha>(sql, new { pdescricao = descricao }).SingleOrDefault();
        }

        public IEnumerable<Campanha> ObterPorSemelhanca(string descricao)
        {
            string sql = @"SELECT * FROM Campanha WHERE Descricao LIKE @pdescricao";
            return _ctx.Database.GetDbConnection().Query<Campanha>(sql, new { pdescricao = $"%{descricao}%" }).ToList();
        }


        #endregion

    }
}
