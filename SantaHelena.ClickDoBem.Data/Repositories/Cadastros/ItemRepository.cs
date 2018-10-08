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
    public class ItemRepository : RepositorioBase<Item>, IItemRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public ItemRepository(CdbContext ctx) : base(ctx) { }

        #endregion

        #region Métodos Públicos

        public override Item ObterPorId(Guid id)
        {
            string sql = @"SELECT * FROM Item WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<Item>(sql, new { pid = id }).SingleOrDefault();
        }

        public override IEnumerable<Item> ObterTodos()
        {
            //TODO: Verificar questão de Gerido pelo RH
            string sql = @"SELECT * FROM Item";
            return _ctx.Database.GetDbConnection().Query<Item>(sql).ToList();
        }

        public Item ObterPorTitulo(string titulo)
        {
            string sql = @"SELECT * FROM Item WHERE Titulo = @ptitulo";
            return _ctx.Database.GetDbConnection().Query<Item>(sql, new { ptitulo = titulo }).SingleOrDefault();
        }

        public IEnumerable<Item> ObterPorSemelhanca(string titulo)
        {
            string sql = @"SELECT * FROM Item WHERE Titulo LIKE @ptitulo";
            return _ctx.Database.GetDbConnection().Query<Item>(sql, new { ptitulo = $"%{titulo}%" }).ToList();
        }

        public IEnumerable<Item> ObterNecessidades()
        {
            //TODO: Verificar questão de Gerido pelo RH
            string sql = @"SELECT i.* FROM Item i INNER JOIN TipoItem ti ON i.TipoItemId = ti.Id WHERE ti.Descricao = 'Necessidade'";
            return _ctx.Database.GetDbConnection().Query<Item>(sql).ToList();
        }

        public IEnumerable<Item> ObterDoacoes()
        {
            //TODO: Verificar questão de Gerido pelo RH
            string sql = @"SELECT i.* FROM Item i INNER JOIN TipoItem ti ON i.TipoItemId = ti.Id WHERE ti.Descricao = 'Doação'";
            return _ctx.Database.GetDbConnection().Query<Item>(sql).ToList();

        }

        #endregion


    }
}
