using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Credenciais
{

    public interface IUsuarioAppService : IAppServiceBase
    {
        bool Autenticar(string usuario, string senha, out string mensagem);
        UsuarioDto ObterPorId(Guid id);
        IEnumerable<UsuarioDto> ObterTodos();
    }

}