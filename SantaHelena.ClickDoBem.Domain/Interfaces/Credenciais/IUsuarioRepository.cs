using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais
{
    public interface IUsuarioRepository : IMySqlRepositoryBase<Usuario>
    {
        Usuario ObterPorLogin(string login, string senha);
        IEnumerable<Usuario> ObterPorPerfil(string perfil);
        Usuario ObterPorDocumento(string documento);
        IEnumerable<Usuario> ObterPorLista(List<Guid> ids);
        void VerificarSituacaoDocumento(string documento, out string situacao, out bool cadastro);
    }
}
