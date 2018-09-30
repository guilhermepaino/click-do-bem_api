using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaHelena.ClickDoBem.Data.Repositories.Cadastros
{
    public class CategoriaRepository : RepositorioBase<Categoria>, ICategoriaRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public CategoriaRepository(CdbContext ctx) : base(ctx) { }

        public override Categoria ObterPorId(Guid id)
        {
            string sql = null;

            sql = @"SELECT * FROM Categoria WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<Categoria>(sql, new { pid = id }).SingleOrDefault();
        }

        public override IEnumerable<Categoria> ObterTodos()
        {
            string sql = @"SELECT * FROM Categoria";
            return _ctx.Database.GetDbConnection().Query<Categoria>(sql).ToList();
        }


        #endregion

    }
}
