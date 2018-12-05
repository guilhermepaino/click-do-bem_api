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
    public class ValorFaixaAppService : AppServiceBase<ValorFaixaDto, ValorFaixa>, IValorFaixaAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly IValorFaixaDomainService _dmn;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public ValorFaixaAppService
        (
            IUnitOfWork uow,
            IValorFaixaDomainService dmn
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
        /// <param name="entidade">Objeto entidade</param>
        protected override ValorFaixaDto ConverterEntidadeEmDto(ValorFaixa entidade)
        {
            return new ValorFaixaDto()
            {
                Id = entidade.Id,
                Descricao = entidade.Descricao,
                ValorInicial = entidade.ValorInicial,
                ValorFinal = entidade.ValorFinal,
                Inativo = entidade.Inativo
            };
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public IEnumerable<ValorFaixaDto> ObterTodos()
        {
            IEnumerable<ValorFaixa> result = _dmn.ObterTodos();
            if (result == null)
                return null;

            return
                (
                    from r in result
                    select ConverterEntidadeEmDto(r)

                ).ToList();
        }

        /// <summary>
        /// Obter registro por id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public ValorFaixaDto ObterPorId(Guid id)
        {

            ValorFaixa result = _dmn.ObterPorId(id);
            if (result == null)
                return null;
            return ConverterEntidadeEmDto(result);
        }

        #endregion

    }
}
