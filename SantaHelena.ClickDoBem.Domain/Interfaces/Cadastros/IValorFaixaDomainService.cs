using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface IValorFaixaDomainService : IDomainServiceBase<ValorFaixa>
    {

        IEnumerable<ValorFaixa> ObterAtivos();

    }
}
