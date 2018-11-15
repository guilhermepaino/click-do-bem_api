using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class CampanhaDomainService : DomainServiceBase<Campanha, ICampanhaRepository>, ICampanhaDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public CampanhaDomainService(ICampanhaRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Buscar Campanha pela descrição (igualdade)
        /// </summary>
        /// <param name="descricao">Descrição a ser localizada</param>
        /// <returns></returns>
        public Campanha ObterPorDescricao(string descricao) => _repository.ObterPorDescricao(descricao);

        /// <summary>
        /// Buscar Campanhas por semelhança (descrição)
        /// </summary>
        /// <param name="descricao">Descrição a ser filtrada</param>
        /// <returns></returns>
        public IEnumerable<Campanha> ObterPorSemelhanca(string descricao) => _repository.ObterPorSemelhanca(descricao);

        /// <summary>
        /// Encerrar uma campanha imediatamente
        /// </summary>
        public void EncerrarCampanha(Guid id) => _repository.EncerrarCampanha(id);

        #endregion

    }
}
