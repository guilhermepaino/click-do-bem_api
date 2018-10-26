﻿using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface ITipoMatchDomainService : IDomainServiceBase<TipoMatch>
    {
        TipoMatch ObterPorDescricao(string descricao);
        IEnumerable<TipoMatch> ObterPorSemelhanca(string descricao);
    }
}
