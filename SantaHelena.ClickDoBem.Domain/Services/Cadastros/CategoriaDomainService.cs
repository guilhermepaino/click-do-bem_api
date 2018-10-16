using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class CategoriaDomainService : DomainServiceBase<Categoria, ICategoriaRepository>, ICategoriaDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public CategoriaDomainService(ICategoriaRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Buscar categoria pela descrição (igualdade)
        /// </summary>
        /// <param name="descricao">Descrição a ser localizada</param>
        /// <returns></returns>
        public Categoria ObterPorDescricao(string descricao) => _repository.ObterPorDescricao(descricao);

        /// <summary>
        /// Buscar categorias por semelhança (descrição)
        /// </summary>
        /// <param name="descricao">Descrição a ser filtrada</param>
        /// <returns></returns>
        public IEnumerable<Categoria> ObterPorSemelhanca(string descricao) => _repository.ObterPorSemelhanca(descricao);

        #endregion

    }
}
