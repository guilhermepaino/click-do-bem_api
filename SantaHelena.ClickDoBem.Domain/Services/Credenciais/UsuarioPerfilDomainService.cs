using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;

namespace SantaHelena.ClickDoBem.Domain.Services.Credenciais
{

    /// <summary>
    /// Objeto de domínio da entidade UsuarioPerfil
    /// </summary>
    public class UsuarioPerfilDomainService : DomainServiceBase<UsuarioPerfil, IUsuarioPerfilRepository>, IUsuarioPerfilDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public UsuarioPerfilDomainService(IUsuarioPerfilRepository repository) : base(repository) { }

        #endregion

    }

}
