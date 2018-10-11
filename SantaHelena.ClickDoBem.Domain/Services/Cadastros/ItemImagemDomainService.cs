using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class ItemImagemDomainService : DomainServiceBase<ItemImagem, IItemImagemRepository>, IItemImagemDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public ItemImagemDomainService(IItemImagemRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Obter registros pela lista de ids de itens informados
        /// </summary>
        /// <param name="listaIds">List de ids de itens</param>
        public IEnumerable<ItemImagem> ObterPorLista(List<Guid> listaIds) => _repository.ObterPorLista(listaIds);

        #endregion

    }
}
