using System;
using System.Collections.Generic;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Core.ReportDto;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface IItemDomainService : IDomainServiceBase<Item>
    {
        IEnumerable<Item> ObterNecessidades();
        IEnumerable<Item> ObterDoacoes();
        IEnumerable<ItemListaReportDto> Pesquisar(DateTime? dataInicial, DateTime? dataFinal, Guid? tipoItemId, Guid? categoriaId);
        bool CarregarImagem(Item item, string nomeImagem, string imagemBase64, string caminho, out object dadosRetorno);
    }
}
