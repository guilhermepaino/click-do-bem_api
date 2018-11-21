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
    public class CampanhaImagemRepository : RepositorioBase<CampanhaImagem>, ICampanhaImagemRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public CampanhaImagemRepository(CdbContext ctx) : base(ctx) { }

        public override CampanhaImagem ObterPorId(Guid id)
        {
            string sql = @"SELECT * FROM CampanhaImagem WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<CampanhaImagem>(sql, new { pid = id }).SingleOrDefault();
        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public override IEnumerable<CampanhaImagem> ObterTodos()
        {
            string sql = @"SELECT * FROM CampanhaImagem";
            return _ctx.Database.GetDbConnection().Query<CampanhaImagem>(sql).ToList();
        }

        /// <summary>
        /// Obter registro pelo Campanha
        /// </summary>
        /// <param name="CampanhaId">Id do Campanha</param>
        public CampanhaImagem ObterPorCampanha(Guid CampanhaId)
        {
            string sql = @"SELECT * FROM CampanhaImagem WHERE CampanhaId = @pCampanhaid";
            return _ctx.Database.GetDbConnection().Query<CampanhaImagem>(sql, new { pCampanhaid = CampanhaId }).FirstOrDefault();
        }

        #endregion

    }
}
