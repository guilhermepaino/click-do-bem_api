﻿using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Cadastros
{
    public interface IValorFaixaAppService : IAppServiceBase
    {
        IEnumerable<ValorFaixaDto> ObterTodos();
        ValorFaixaDto ObterPorId(Guid id);
    }
}
