using System;
using System.Collections.Generic;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Core.ReportDto;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface IItemRepository : IMySqlRepositoryBase<Item>
    {
        IEnumerable<Item> ObterTodos(bool incluirMatches);
        IEnumerable<Item> ObterNecessidades(bool incluirMatches);
        IEnumerable<Item> ObterDoacoes(bool incluirMatches);
        IEnumerable<ItemListaReportDto> Pesquisar(DateTime? dataInicial, DateTime? dataFinal, Guid? tipoItemId, Guid? categoriaId);
        IEnumerable<Item> PesquisarParaMatche(DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId);
        IEnumerable<ItemMatchReportDto> ListarMatches(Guid usuarioId, DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId, bool? efetivados);
        IEnumerable<ItemMatchReportDto> ListarMatches(Guid usuarioId);
        IEnumerable<RankingIndividualReportDto> RankingIndividual();
        IEnumerable<RankingCampanhaReportDto> RankingCampanha();

    }
}
