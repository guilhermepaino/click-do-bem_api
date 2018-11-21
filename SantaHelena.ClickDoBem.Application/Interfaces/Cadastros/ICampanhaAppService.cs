using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Cadastros
{
    public interface ICampanhaAppService : IAppServiceBase
    {
        IEnumerable<CampanhaDto> ObterTodos();
        IEnumerable<CampanhaDto> ObterAtivas();
        CampanhaDto ObterPorId(Guid id);
        void Inserir(CampanhaDto dto, out int statusCode, out object mensagem);
        void Atualizar(CampanhaDto dto, out int statusCode, out string mensagem);
        void Excluir(Guid id, string webRootPath, out int statusCode, out object dados);
        void EncerrarCampanha(Guid id, out int statusCode, out object dados);
        void CarregarImagem(Guid campanhaId, string imagemBase64, string caminho, out int statusCode, out object dadosRetorno);
    }
}
