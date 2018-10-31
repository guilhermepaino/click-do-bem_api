using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Domain.Core.ReportDto;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Cadastros
{
    public interface IItemAppService : IAppServiceBase
    {

        IEnumerable<ItemDto> ObterTodos(bool incluirItensMatches);
        ItemDto ObterPorId(Guid id);
        IEnumerable<ItemDto> ObterDoacoes();
        IEnumerable<ItemDto> ObterNecessidades();
        IEnumerable<ItemListaReportDto> Pesquisar(DateTime? dataInicial, DateTime? dataFinal, Guid? tipoItemId, Guid? categoriaId);
        IEnumerable<ItemDto> ListarLivresParaMatches(DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId);
        IEnumerable<ItemMatchReportDto> ListarMatches(DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId, bool? efetivados);
        void Inserir(ItemDto dto, out int statusCode, out string mensagem);
        void Atualizar(ItemDto dto, out int statusCode, out string mensagem);
        void Excluir(Guid id, string pastaWwwRoot, out int statusCode, out object dados);
        void CarregarImagem(Guid itemId, string nomeImagem, string imagemBase64, string caminho, out int statusCode, out object dadosRetorno);
        void RemoverImagem(Guid id, string caminho, out int statusCode, out object dadosRetorno);
        void ExecutarMatch(Guid doacaoId, Guid necessidadeId, out int statusCode, out object dadosRetorno);
        void DesfazerMatch(Guid id, out int statusCode, out object dadosRetorno);
        void ExecutarMatch(Guid id, decimal valor, out int statusCode, out object dadosRetorno);
    }
}
