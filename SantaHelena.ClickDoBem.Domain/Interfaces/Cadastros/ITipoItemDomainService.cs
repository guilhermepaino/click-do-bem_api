using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface ITipoItemDomainService : IDomainServiceBase<TipoItem>
    {
        TipoItem ObterPorDescricao(string descricao);
        IEnumerable<TipoItem> ObterPorSemelhanca(string descricao);
    }
}
