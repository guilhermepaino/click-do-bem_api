using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Cadastros
{
    public interface ICategoriaAppService : IAppServiceBase
    {
        IEnumerable<CategoriaDto> ObterTodos();
        CategoriaDto ObterPorId(Guid id);
    }
}
