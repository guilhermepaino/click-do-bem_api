﻿using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Cadastros
{
    public interface ITipoItemAppService : IAppServiceBase
    {
        IEnumerable<TipoItemDto> ObterTodos();
    }
}
