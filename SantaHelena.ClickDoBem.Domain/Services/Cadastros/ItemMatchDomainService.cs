using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class ItemMatchDomainService : DomainServiceBase<ItemMatch, IItemMatchRepository>, IItemMatchDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public ItemMatchDomainService(IItemMatchRepository repository) : base(repository) { }

        #endregion

        #region Métodos Públicos

        public ItemMatch BuscarPorMatch(Guid doacaoId, Guid necessidadeId) => _repository.BuscarPorMatch(doacaoId, necessidadeId);

        #endregion

    }
}
