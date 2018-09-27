using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Text;

namespace SantaHelena.ClickDoBem.Domain.Services.Credenciais
{

    /// <summary>
    /// Objeto de domínio da entidade Usuario
    /// </summary>
    public class UsuarioDomainService : DomainServiceBase<Usuario, IUsuarioRepository>, IUsuarioDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public UsuarioDomainService(IUsuarioRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        public Usuario ObterPorUsuarioSenha(string usuario, string senha) => _repository.ObterPorUsuarioSenha(usuario, senha);

        #endregion

    }

}
