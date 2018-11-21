using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface ICampanhaImagemRepository : IMySqlRepositoryBase<CampanhaImagem>
    {
        CampanhaImagem ObterPorCampanha(Guid CampanhaId);
    }
}
