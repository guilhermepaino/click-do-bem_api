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

        #region Métodos Locais

        protected IEnumerable<ItemMatchReportDto> ListagemMatch(Guid usuarioId, DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId, bool? efetivados, bool filtrarUsuario)
        {

            string adm = filtrarUsuario ? "0" : "1";

            string sql = $@"SELECT
                                im.Id,
                                im.Data,
                                tm.Descricao TipoMatch,
                                (CASE WHEN di.Anonimo = 1 AND du.Id <> @puid AND {adm} = 0 THEN  '** ANÔNIMO **' ELSE du.Nome END) NomeDoador,
                                (CASE WHEN di.Anonimo = 1 AND du.Id <> @puid AND {adm} = 0 THEN '--' ELSE dud.TelefoneFixo END) TelefoneDoador,
                                (CASE WHEN di.Anonimo = 1 AND du.Id <> @puid AND {adm} = 0 THEN '--' ELSE dud.TelefoneCelular END) CelularDoador,
                                (CASE WHEN di.Anonimo = 1 AND du.Id <> @puid AND {adm} = 0 THEN '--' ELSE dud.Email END) EmailDoador,
                                (CASE WHEN ni.Anonimo = 1 AND nu.Id <> @puid AND {adm} = 0 THEN  '** ANÔNIMO **' ELSE nu.Nome END) NomeReceptor,
                                (CASE WHEN ni.Anonimo = 1 AND nu.Id <> @puid AND {adm} = 0 THEN '--' ELSE nud.TelefoneFixo END) TelefoneReceptor,
                                (CASE WHEN ni.Anonimo = 1 AND nu.Id <> @puid AND {adm} = 0 THEN '--' ELSE nud.TelefoneCelular END) CelularReceptor,
                                (CASE WHEN ni.Anonimo = 1 AND nu.Id <> @puid AND {adm} = 0 THEN '--' ELSE nud.Email END) EmailReceptor,
                                (CASE WHEN im.TipoMatchId = 'b69eed4f-d87c-11e8-abfa-0e0e947bb2d6' THEN ni.Titulo ELSE di.Titulo END) Titulo,
                                (CASE WHEN im.TipoMatchId = 'b69eed4f-d87c-11e8-abfa-0e0e947bb2d6' THEN ni.Descricao ELSE di.Descricao END) Descricao,
                                dc.Descricao Categoria,
                                vf.Descricao ValorFaixa,
                                dc.Pontuacao,
                                dc.GerenciadaRh,
                                im.Efetivado,
                                IFNULL(imgd.Imagem, imgn.Imagem) Imagem
                            FROM ItemMatch im
                            INNER JOIN Item di ON im.DoacaoId = di.Id
                            INNER JOIN Usuario du ON di.UsuarioId = du.Id
                            INNER JOIN UsuarioDados dud ON du.Id = dud.UsuarioId
                            INNER JOIN Categoria dc ON di.CategoriaId = dc.Id
                            INNER JOIN Item ni ON im.NecessidadeId = ni.Id
                            INNER JOIN Usuario nu ON ni.UsuarioId = nu.Id
                            INNER JOIN UsuarioDados nud ON nu.Id = nud.UsuarioId
                            INNER JOIN TipoMatch tm ON im.TipoMatchId = tm.Id
                            LEFT JOIN
                            (
                                SELECT ii.ItemId, MIN(ii.Caminho) Imagem
                                FROM ItemImagem ii
                                GROUP BY ii.ItemId
                            ) imgd ON di.Id = imgd.ItemId
                            LEFT JOIN
                            (
                                SELECT ii.ItemId, MIN(ii.Caminho) Imagem
                                FROM ItemImagem ii
                                GROUP BY ii.ItemId
                            ) imgn ON ni.Id = imgn.ItemId
                            LEFT JOIN ValorFaixa vf ON im.ValorFaixaId = vf.Id ";

            if (filtrarUsuario)
            {
                sql = $@"{sql}
                            WHERE
                                du.Id = @puid OR nu.Id = @puid
                            ORDER BY im.Data DESC";
            }
            else
            {


                sql = $@"{sql} 
                            WHERE
                                (im.Data BETWEEN _DATAINICIAL_ AND _DATAFINAL_)
                                AND dc.Id = _CATEGORIAID_
                                AND im.Efetivado = _EFETIVADO_";

                if (dataInicial.HasValue && dataFinal.HasValue)
                {
                    sql = sql
                        .Replace("_DATAINICIAL_", $"'{dataInicial.Value.ToString("yyyy-MM-dd")} 00:00:00'")
                        .Replace("_DATAFINAL_", $"'{dataFinal.Value.ToString("yyyy-MM-dd")} 23:59:59'");
                }
                else
                    sql = sql
                        .Replace("_DATAINICIAL_", "im.Data")
                        .Replace("_DATAFINAL_", "im.Data");

                if (categoriaId.HasValue)
                    sql = sql.Replace("_CATEGORIAID_", $"'{categoriaId.ToString()}'");
                else
                    sql = sql.Replace("_CATEGORIAID_", "dc.Id");

                if (efetivados.HasValue)
                    sql = sql.Replace("_EFETIVADO_", $"{(efetivados.Value ? 1 : 0)}");
                else
                    sql = sql.Replace("_EFETIVADO_", "im.Efetivado");

                sql = $"{sql} ORDER BY im.Data ASC";

            }

            return _ctx.Database.GetDbConnection().Query<ItemMatchReportDto>(sql, new { puid = usuarioId.ToString() }).ToList();

        }

        #endregion

        #region Métodos Públicos

        public IEnumerable<Item> ObterTodos(bool incluirMatches)
        {
            string sql = @"SELECT i.* FROM Item i WHERE i.GeradoPorMatch = 0";
            if (!incluirMatches)
                sql = $"{sql} AND NOT EXISTS(SELECT 1 FROM ItemMatch im WHERE im.NecessidadeId = i.Id || im.DoacaoId = i.Id)";
            sql = $"{sql} ORDER BY i.DataInclusao DESC";
            return _ctx.Database.GetDbConnection().Query<Item>(sql).ToList();
        }

        public override Item ObterPorId(Guid id)
        {
            string sql = @"SELECT * FROM Item WHERE Id = @pid ORDER BY DataInclusao DESC";
            return _ctx.Database.GetDbConnection().Query<Item>(sql, new { pid = id }).SingleOrDefault();
        }

        public override IEnumerable<Item> ObterTodos()
        {
            string sql = @"SELECT * FROM Item ORDER BY DataInclusao DESC";
            return _ctx.Database.GetDbConnection().Query<Item>(sql).ToList();
        }

        public Item ObterPorTitulo(string titulo)
        {
            string sql = @"SELECT * FROM Item WHERE Titulo = @ptitulo ORDER BY DataInclusao DESC";
            return _ctx.Database.GetDbConnection().Query<Item>(sql, new { ptitulo = titulo }).SingleOrDefault();
        }

        public IEnumerable<Item> ObterPorSemelhanca(string titulo)
        {
            string sql = @"SELECT * FROM Item WHERE Titulo LIKE @ptitulo ORDER BY DataInclusao DESC";
            return _ctx.Database.GetDbConnection().Query<Item>(sql, new { ptitulo = $"%{titulo}%" }).ToList();
        }

        public IEnumerable<Item> ObterNecessidades(bool incluirMatches)
        {
            string sql = @"SELECT i.* FROM Item i INNER JOIN TipoItem ti ON i.TipoItemId = ti.Id WHERE ti.Descricao = 'Necessidade' AND i.GeradoPorMatch = 0";
            if (!incluirMatches)
                sql = $"{sql} AND NOT EXISTS(SELECT 1 FROM ItemMatch im WHERE im.NecessidadeId = i.Id || im.DoacaoId = i.Id)";
            sql = $"{sql} ORDER BY i.DataInclusao DESC";
            return _ctx.Database.GetDbConnection().Query<Item>(sql).ToList();
        }

        public IEnumerable<Item> ObterDoacoes(bool incluirMatches)
        {
            string sql = @"SELECT i.* FROM Item i INNER JOIN TipoItem ti ON i.TipoItemId = ti.Id WHERE ti.Descricao = 'Doação' AND i.GeradoPorMatch = 0";
            if (!incluirMatches)
                sql = $"{sql} AND NOT EXISTS(SELECT 1 FROM ItemMatch im WHERE im.NecessidadeId = i.Id || im.DoacaoId = i.Id)";
            sql = $"{sql} ORDER BY i.DataInclusao DESC";
            return _ctx.Database.GetDbConnection().Query<Item>(sql).ToList();

        }

        public IEnumerable<ItemListaReportDto> Pesquisar(DateTime? dataInicial, DateTime? dataFinal, Guid? tipoItemId, Guid? categoriaId)
        {

            string sql = $@"SELECT
                              s.Id,
                              s.TipoItem,
                              s.DataInclusao,
                              s.DataEfetivacao,
                              s.Doador,
                              s.TelefoneDoador,
                              s.CelularDoador,
                              s.EmailDoador,
                              s.Receptor,
                              s.TelefoneReceptor,
                              s.CelularReceptor,
                              s.EmailReceptor,
                              s.Titulo,
                              s.Descricao,
                              s.Categoria,
                              s.Pontuacao Peso,
                              s.ValorFaixa,
                              s.GerenciadaRh
                            FROM
                            (
                              SELECT
                                i.Id,
                                ti.Descricao TipoItem,
                                i.DataInclusao,
                                im.Data DataEfetivacao,
                                u.Nome Doador,
                                ud.TelefoneFixo TelefoneDoador,
                                ud.TelefoneCelular CelularDoador,
                                ud.Email EmailDoador,
                                un.Nome Receptor,
                                und.TelefoneFixo TelefoneReceptor,
                                und.TelefoneCelular CelularReceptor,
                                und.Email EmailReceptor,
                                i.Titulo,
                                i.Descricao,
                                c.Descricao Categoria,
                                c.Pontuacao,
                                vf.Descricao ValorFaixa,
                                c.GerenciadaRh
                              FROM Item i
                                  INNER JOIN TipoItem ti ON i.TipoItemId = ti.Id
                                  INNER JOIN Categoria c ON i.CategoriaId = c.Id
                                  INNER JOIN Usuario u ON i.UsuarioId = u.Id
                                  INNER JOIN UsuarioDados ud ON u.Id = ud.UsuarioId
                                  LEFT JOIN ItemMatch im ON i.Id = im.DoacaoId
                                  LEFT JOIN Usuario un ON im.UsuarioId = un.Id
                                  LEFT JOIN UsuarioDados und ON un.Id = und.UsuarioId
                                  LEFT JOIN ValorFaixa vf ON im.ValorFaixaId = vf.Id
                              WHERE 
                                ti.Descricao = 'Doação' 
                                AND i.GeradoPorMatch = 0
                                AND (i.DataInclusao BETWEEN _DATAINICIAL_ AND _DATAFINAL_)
                                AND ti.Id = _TIPOITEMID_
                                AND c.Id = _CATEGORIAID_
  
                              UNION ALL
  
                              SELECT
                                i.Id,
                                ti.Descricao TipoItem,
                                i.DataInclusao,
                                im.Data DataEfetivacao,
                                ud.Nome Doador,
                                udd.TelefoneFixo TelefoneDoador,
                                udd.TelefoneCelular CelularDoador,
                                udd.Email EmailDoador,
                                u.Nome Receptor,
                                udn.TelefoneFixo TelefoneReceptor,
                                udn.TelefoneCelular CelularReceptor,
                                udn.Email EmailReceptor,
                                i.Titulo,
                                i.Descricao,
                                c.Descricao Categoria,
                                c.Pontuacao,
                                vf.Descricao ValorFaixa,
                                c.GerenciadaRh
                              FROM Item i
                                  INNER JOIN TipoItem ti ON i.TipoItemId = ti.Id
                                  INNER JOIN Categoria c ON i.CategoriaId = c.Id
                                  INNER JOIN Usuario u ON i.UsuarioId = u.Id
                                  INNER JOIN UsuarioDados udn ON u.Id = udn.UsuarioId
                                  LEFT JOIN ItemMatch im ON i.Id = im.NecessidadeId
                                  LEFT JOIN Usuario ud ON im.UsuarioId = ud.Id
                                  LEFT JOIN UsuarioDados udd ON ud.Id = udd.UsuarioId
                                  LEFT JOIN ValorFaixa vf ON im.ValorFaixaId = vf.Id
                              WHERE 
                                ti.Descricao = 'Necessidade' 
                                AND i.GeradoPorMatch = 0
                                AND (i.DataInclusao BETWEEN _DATAINICIAL_ AND _DATAFINAL_)
                                AND ti.Id = _TIPOITEMID_
                                AND c.Id = _CATEGORIAID_
                            ) s";

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

            sql = $"{sql} ORDER BY s.DataInclusao DESC";

            return _ctx.Database.GetDbConnection().Query<ItemListaReportDto>(sql).ToList();

        }

        public IEnumerable<Item> PesquisarParaMatche(DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId)
        {

            string sql = $@"SELECT i.*
                            FROM Item i
                            INNER JOIN Categoria c ON i.CategoriaId = c.Id
                            INNER JOIN Usuario u ON i.UsuarioId = u.Id
                            WHERE
                              (i.DataInclusao BETWEEN _DATAINICIAL_ AND _DATAFINAL_)
                              AND c.Id = _CATEGORIAID_
                              AND NOT EXISTS(SELECT 1 FROM ItemMatch im WHERE i.Id = im.DoacaoId OR i.Id = im.NecessidadeId)";

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

            if (categoriaId != null)
                sql = sql.Replace("_CATEGORIAID_", $"'{categoriaId.ToString()}'");
            else
                sql = sql.Replace("_CATEGORIAID_", "c.Id");

            sql = $"{sql} ORDER BY i.DataInclusao DESC";

            return _ctx.Database.GetDbConnection().Query<Item>(sql).ToList();

        }

        public IEnumerable<ItemMatchReportDto> ListarMatches(Guid usuarioId)
        {
            return ListagemMatch(usuarioId, null, null, null, null, true);
        }

        public IEnumerable<ItemMatchReportDto> ListarMatches(Guid usuarioId, DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId, bool? efetivados)
        {
            return ListagemMatch(usuarioId, dataInicial, dataFinal, categoriaId, efetivados, false);
        }

        public IEnumerable<RankingIndividualReportDto> RankingIndividual()
        {

            string sql = $@"SELECT s.UsuarioId, s.Nome, s.Pontuacao
                            FROM
                            (
                                SELECT u.Id UsuarioId, u.Nome, SUM(c.Pontuacao) Pontuacao, COUNT(im.Id) QtdDoacoes
                                FROM ItemMatch im
                                INNER JOIN Item i ON im.DoacaoId = i.Id
                                INNER JOIN Usuario u ON i.UsuarioId = u.Id
                                INNER JOIN Categoria c ON i.CategoriaId = c.Id
                                WHERE i.Anonimo = 0
                                GROUP BY u.Id, u.Nome
                            ) s
                            ORDER BY s.Pontuacao DESC, s.QtdDoacoes ASC, s.Nome ASC
                            LIMIT 100";

            return _ctx.Database.GetDbConnection().Query<RankingIndividualReportDto>(sql).ToList();

        }

        public IEnumerable<RankingCampanhaReportDto> RankingCampanha()
        {
            string sql = $@"SELECT s.CampanhaId, s.Descricao, s.Pontuacao, s.DataInicial, s.DataFinal
                            FROM
                            (
                                SELECT i.CampanhaId, IFNULL(cmp.Descricao, '-- SEM CAMPANHA -- ') Descricao, SUM(c.Pontuacao) Pontuacao, COUNT(im.Id) QtdDoacoes, cmp.DataInicial, cmp.DataFinal
                                FROM ItemMatch im
                                INNER JOIN Item i ON im.DoacaoId = i.Id
                                INNER JOIN Categoria c ON i.CategoriaId = c.Id
                                LEFT JOIN Campanha cmp ON i.CampanhaId = cmp.Id
                                WHERE i.Anonimo = 0
                                GROUP BY i.CampanhaId, cmp.Descricao, cmp.DataInicial, cmp.DataFinal
                            ) s
                            ORDER BY (CASE WHEN s.CampanhaId IS NULL THEN -999 ELSE s.Pontuacao END) DESC, s.QtdDoacoes ASC, 2 ASC
                            LIMIT 100";

            return _ctx.Database.GetDbConnection().Query<RankingCampanhaReportDto>(sql).ToList();
        }

        #endregion


    }
}
