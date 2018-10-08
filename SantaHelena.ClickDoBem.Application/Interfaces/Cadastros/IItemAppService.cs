using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Cadastros
{
    public interface IItemAppService : IAppServiceBase
    {
        IEnumerable<ItemDto> ObterTodos();
        void Inserir(ItemDto dto, out int statusCode, out object dados);
        void Atualizar(ItemDto dto, out int statusCode, out object dados);
        void Excluir(Guid id, out int statusCode, out object dados);
        ItemDto ObterPorId(Guid id);
        IEnumerable<ItemDto> ObterDoacoes();
        IEnumerable<ItemDto> ObterNecessidades();
    }
}
