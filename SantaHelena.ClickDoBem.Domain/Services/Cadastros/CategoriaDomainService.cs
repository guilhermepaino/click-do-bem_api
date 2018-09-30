using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;

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


        #endregion

    }
}
