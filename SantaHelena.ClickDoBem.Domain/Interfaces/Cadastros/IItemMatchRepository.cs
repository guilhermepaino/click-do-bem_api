using System;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface IItemMatchRepository : IMySqlRepositoryBase<ItemMatch>
    {
        ItemMatch BuscarPorMatch(Guid doacaoId, Guid necessidadeId);
    }
}
