using System.Collections.Generic;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface IItemRepository : IMySqlRepositoryBase<Item>
    {
        IEnumerable<Item> ObterNecessidades();
        IEnumerable<Item> ObterDoacoes();
    }
}
