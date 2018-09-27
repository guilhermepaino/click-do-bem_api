using SantaHelena.ClickDoBem.Application.ViewModels.Credenciais;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Credenciais
{

    public interface IUsuarioAppService : IAppServiceBase
    {
        bool Autenticar(string usuario, string senha, out string mensagem);
        IEnumerable<UsuarioAppViewModel> ObterTodos();

    }

}