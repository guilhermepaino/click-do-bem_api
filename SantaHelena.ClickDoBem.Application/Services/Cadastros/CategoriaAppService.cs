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
    public class CategoriaAppService : AppServiceBase<CategoriaDto, Categoria>, ICategoriaAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly ICategoriaDomainService _dmn;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public CategoriaAppService
        (
            IUnitOfWork uow,
            ICategoriaDomainService dmn
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
        /// <param name="categoria">Objeto entidade</param>
        protected override CategoriaDto ConverterEntidadeEmDto(Categoria categoria)
        {
            return new CategoriaDto()
            {
                Id = categoria.Id,
                DataInclusao = categoria.DataInclusao,
                DataAlteracao = categoria.DataAlteracao,
                Descricao = categoria.Descricao,
                Pontuacao = categoria.Pontuacao,
                GerenciadaRh = categoria.GerenciadaRh
            };
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public IEnumerable<CategoriaDto> ObterTodos()
        {
            IEnumerable<Categoria> result = _dmn.ObterTodos();
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
        public CategoriaDto ObterPorId(Guid id)
        {
            Categoria result = _dmn.ObterPorId(id);
            if (result == null)
                return null;
            return ConverterEntidadeEmDto(result);
        }

        #endregion

    }
}
