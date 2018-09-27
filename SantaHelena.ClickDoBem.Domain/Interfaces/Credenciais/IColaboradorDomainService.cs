using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais
{

    public interface IColaboradorDomainService : IDomainServiceBase<Colaborador>
    {

        Colaborador ObterPorCpf(string cpf);
        IEnumerable<Colaborador> ObterTodos();

    }

}