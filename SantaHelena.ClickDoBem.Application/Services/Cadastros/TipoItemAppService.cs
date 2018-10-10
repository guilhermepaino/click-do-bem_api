using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Application.Services.Cadastros
{
    public class TipoItemAppService : AppServiceBase<TipoItemDto, TipoItem>, ITipoItemAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly ITipoItemDomainService _dmn;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public TipoItemAppService
        (
            IUnitOfWork uow,
            ITipoItemDomainService dmn
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
        /// <param name="TipoItem">Objeto entidade</param>
        protected override TipoItemDto ConverterEntidadeEmDto(TipoItem tipoItem)
        {
            return new TipoItemDto()
            {
                Id = tipoItem.Id,
                DataInclusao = tipoItem.DataInclusao,
                DataAlteracao = tipoItem.DataAlteracao,
                Descricao = tipoItem.Descricao
            };
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public IEnumerable<TipoItemDto> ObterTodos()
        {
            IEnumerable<TipoItem> result = _dmn.ObterTodos();
            if (result == null)
                return null;

            return
                (
                    from r in result
                    select ConverterEntidadeEmDto(r)

                ).ToList();
        }

        #endregion

    }
}
