using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais
{

    public interface IUsuarioDomainService : IDomainServiceBase<Usuario>
    {

        Usuario ObterPorLogin(string login, string senha);
        Usuario ObterPorDocumento(string documento);
        IEnumerable<Usuario> ObterPorPerfil(string perfil);
        IEnumerable<Usuario> ObterPorLista(List<Guid> ids);
        void VerificarSituacaoDocumento(string documento, out string situacao, out bool cadastrado);
    }

}