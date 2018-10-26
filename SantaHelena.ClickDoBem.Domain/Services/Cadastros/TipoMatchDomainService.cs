using System.Collections.Generic;
using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class TipoMatchDomainService : DomainServiceBase<TipoMatch, ITipoMatchRepository>, ITipoMatchDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public TipoMatchDomainService(ITipoMatchRepository repository) : base(repository) { }

        /// <summary>
        /// Obter registro pela descrição (igualdade)
        /// </summary>
        /// <param name="descricao">Descrição a ser localizada</param>
        public TipoMatch ObterPorDescricao(string descricao) => _repository.ObterPorDescricao(descricao);

        /// <summary>
        /// Obter registro por semelhança (descrição)
        /// </summary>
        /// <param name="descricao">Descrição a ser localizada</param>
        public IEnumerable<TipoMatch> ObterPorSemelhanca(string descricao) => _repository.ObterPorSemelhanca(descricao);

        #endregion

        #region Métodos públicos


        #endregion

    }
}
