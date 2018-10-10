using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Core.ReportDto;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class ItemDomainService : DomainServiceBase<Item, IItemRepository>, IItemDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public ItemDomainService(IItemRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Obter registros de necessidades
        /// </summary>
        public IEnumerable<Item> ObterNecessidades() => _repository.ObterNecessidades();

        /// <summary>
        /// Obter registros de doações
        /// </summary>
        public IEnumerable<Item> ObterDoacoes() => _repository.ObterDoacoes();

        /// <summary>
        /// Pesquisar itens com base nos filtros informados
        /// </summary>
        /// <param name="dataInicial">Data inicial do período</param>
        /// <param name="dataFinal">Data final do período</param>
        /// <param name="tipoItemId">Id do tipo de item</param>
        /// <param name="categoriaId">Id da categoria</param>
        public IEnumerable<ItemListaReportDto> Pesquisar(DateTime? dataInicial, DateTime? dataFinal, Guid? tipoItemId, Guid? categoriaId) 
            => _repository.Pesquisar(dataInicial, dataFinal, tipoItemId, categoriaId);

        #endregion

    }
}
