using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais
{

    public interface IColaboradorDomainService : IDomainServiceBase<Colaborador>
    {

        Colaborador ObterPorCpf(string cpf);

    }

}