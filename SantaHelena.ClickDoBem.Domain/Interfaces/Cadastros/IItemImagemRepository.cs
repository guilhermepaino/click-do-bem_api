using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface IItemImagemRepository : IMySqlRepositoryBase<ItemImagem>
    {
        IEnumerable<ItemImagem> ObterPorItem(Guid itemId);
    }
}
