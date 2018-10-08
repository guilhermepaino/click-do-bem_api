using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
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

        #endregion

    }
}
