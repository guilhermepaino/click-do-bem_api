using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;

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

    }
}
