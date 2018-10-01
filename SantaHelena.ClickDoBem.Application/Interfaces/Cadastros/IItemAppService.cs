using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Cadastros
{
    public interface IItemAppService : IAppServiceBase
    {
        IEnumerable<ItemDto> ObterTodos();
        bool Inserir(ItemDto dto, out int statusCode, out object dados);
        bool Atualizar(ItemDto dto, out int statusCode, out object dados);
    }
}
