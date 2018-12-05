using System;
using System.Collections.Generic;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface ICampanhaImagemDomainService : IDomainServiceBase<CampanhaImagem>
    {
        CampanhaImagem ObterPorCampanha(Guid CampanhaId);
    }
}
