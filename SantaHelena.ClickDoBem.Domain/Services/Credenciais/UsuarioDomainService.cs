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

        public Usuario ObterPorLogin(string login, string senha) => _repository.ObterPorLogin(login, senha);
        public Usuario ObterPorDocumento(string documento) => _repository.ObterPorDocumento(documento);
        public IEnumerable<Usuario> ObterPorPerfil(string perfil) => _repository.ObterPorPerfil(perfil);
        public IEnumerable<Usuario> ObterPorLista(List<Guid> ids) => _repository.ObterPorLista(ids);
        public void VerificarSituacaoDocumento(string documento, out string situacao, out bool cadastrado) => _repository.VerificarSituacaoDocumento(documento, out situacao, out cadastrado);

        #endregion

    }

}
