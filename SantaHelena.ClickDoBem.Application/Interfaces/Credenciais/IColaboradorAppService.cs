using SantaHelena.ClickDoBem.Application.ViewModels.Credenciais;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Credenciais
{

    public interface IColaboradorAppService : IAppServiceBase
    {

        IEnumerable<ColaboradorAppViewModel> ObterTodos();
        ColaboradorAppViewModel ObterPorCpf(string cpf);

    }

}