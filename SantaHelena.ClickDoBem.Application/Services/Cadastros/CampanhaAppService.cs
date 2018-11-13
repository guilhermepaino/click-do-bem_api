using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Application.Services.Cadastros
{
    public class CampanhaAppService : AppServiceBase<CampanhaDto, Campanha>, ICampanhaAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly ICampanhaDomainService _dmn;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public CampanhaAppService
        (
            IUnitOfWork uow,
            ICampanhaDomainService dmn
        )
        {
            _uow = uow;
            _dmn = dmn;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Converter entidade em Dto
        /// </summary>
        /// <param name="campanha">Objeto entidade</param>
        protected override CampanhaDto ConverterEntidadeEmDto(Campanha campanha)
        {
            return new CampanhaDto()
            {
                Id = campanha.Id,
                DataInclusao = campanha.DataInclusao,
                DataAlteracao = campanha.DataAlteracao,
                Descricao = campanha.Descricao,
                DataInicial = campanha.DataInicial,
                DataFinal = campanha.DataFinal
            };
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public IEnumerable<CampanhaDto> ObterTodos()
        {
            IEnumerable<Campanha> result = _dmn.ObterTodos();
            if (result == null)
                return null;

            return
                (
                    from r in result
                    select ConverterEntidadeEmDto(r)

                ).ToList();
        }

        /// <summary>
        /// Obter registro pelo id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public CampanhaDto ObterPorId(Guid id)
        {
            Campanha result = _dmn.ObterPorId(id);
            if (result == null)
                return null;
            return ConverterEntidadeEmDto(result);
        }

        #endregion

    }
}
