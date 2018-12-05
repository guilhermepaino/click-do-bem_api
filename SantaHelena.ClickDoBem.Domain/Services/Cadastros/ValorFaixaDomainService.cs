using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class ValorFaixaDomainService : DomainServiceBase<ValorFaixa, IValorFaixaRepository>, IValorFaixaDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public ValorFaixaDomainService(IValorFaixaRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Obter registros ativos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValorFaixa> ObterAtivos()
        {
            return _repository.Obter(x => !x.Inativo).ToList().OrderBy(o => o.ValorInicial);
        }

        #endregion

    }
}
