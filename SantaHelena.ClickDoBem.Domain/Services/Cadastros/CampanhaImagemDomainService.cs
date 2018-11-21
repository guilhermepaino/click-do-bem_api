using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class CampanhaImagemDomainService : DomainServiceBase<CampanhaImagem, ICampanhaImagemRepository>, ICampanhaImagemDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public CampanhaImagemDomainService(ICampanhaImagemRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Obter registro pela campanha
        /// </summary>
        /// <param name="CampanhaId">Id da campanha</param>
        public CampanhaImagem ObterPorCampanha(Guid CampanhaId) => _repository.ObterPorCampanha(CampanhaId);

        #endregion

    }
}
