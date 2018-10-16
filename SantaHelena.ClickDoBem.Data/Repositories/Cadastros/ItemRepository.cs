using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Core.ReportDto;
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

        public IEnumerable<ItemListaReportDto> Pesquisar(DateTime? dataInicial, DateTime? dataFinal, Guid? tipoItemId, Guid? categoriaId)
        {

            string sql = $@"SELECT
                              ti.Descricao TipoItem,
                              i.DataInclusao,
                              NULL DataEfetivacao,
                              (CASE WHEN i.Anonimo THEN '- ANÔNIMO -' ELSE u.Nome END) Doador,
                              '<//TODO: Apos Doacao concluida>' Receptor,
                              i.Titulo,
                              i.Descricao,
                              c.Descricao Categoria,
                              c.Pontuacao Peso,
                              c.GerenciadaRh
                            FROM Item i
                            INNER JOIN TipoItem ti ON i.TipoItemId = ti.Id
                            INNER JOIN Categoria c ON i.CategoriaId = c.Id
                            INNER JOIN Usuario u ON i.UsuarioId = u.Id
                            WHERE
                              (i.DataInclusao BETWEEN _DATAINICIAL_ AND _DATAFINAL_)
                              AND ti.Id = _TIPOITEMID_
                              AND c.Id = _CATEGORIAID_";

            //TODO: Revisitar após efetivar doação

            if (dataInicial != null && dataFinal != null)
            {
                sql = sql
                    .Replace("_DATAINICIAL_", $"'{dataInicial.Value.ToString("yyyy-MM-dd")} 00:00:00'")
                    .Replace("_DATAFINAL_", $"'{dataFinal.Value.ToString("yyyy-MM-dd")} 23:59:59'");
            }
            else
                sql = sql
                    .Replace("_DATAINICIAL_", "i.DataInclusao")
                    .Replace("_DATAFINAL_", "i.DataInclusao");

            if (tipoItemId != null)
                sql = sql.Replace("_TIPOITEMID_", $"'{tipoItemId.ToString()}'");
            else
                sql = sql.Replace("_TIPOITEMID_", "ti.Id");

            if (categoriaId != null)
                sql = sql.Replace("_CATEGORIAID_", $"'{categoriaId.ToString()}'");
            else
                sql = sql.Replace("_CATEGORIAID_", "c.Id");

            return _ctx.Database.GetDbConnection().Query<ItemListaReportDto>(sql).ToList();

        }

        #endregion


    }
}
