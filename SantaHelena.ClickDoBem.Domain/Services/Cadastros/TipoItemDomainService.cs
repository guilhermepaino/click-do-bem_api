using System.Collections.Generic;
using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class TipoItemDomainService : DomainServiceBase<TipoItem, ITipoItemRepository>, ITipoItemDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public TipoItemDomainService(ITipoItemRepository repository) : base(repository) { }

        /// <summary>
        /// Obter registro pela descrição (igualdade)
        /// </summary>
        /// <param name="descricao">Descrição a ser localizada</param>
        public TipoItem ObterPorDescricao(string descricao) => _repository.ObterPorDescricao(descricao);

        /// <summary>
        /// Obter registro por semelhança (descrição)
        /// </summary>
        /// <param name="descricao">Descrição a ser localizada</param>
        public IEnumerable<TipoItem> ObterPorSemelhanca(string descricao) => _repository.ObterPorSemelhanca(descricao);

        #endregion

        #region Métodos públicos


        #endregion

    }
}
