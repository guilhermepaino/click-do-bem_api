using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros
{
    public interface ICampanhaDomainService : IDomainServiceBase<Campanha>
    {
        Campanha ObterPorDescricao(string descricao);
        IEnumerable<Campanha> ObterPorSemelhanca(string descricao);
        void EncerrarCampanha(Guid id);
        bool CarregarImagem(Campanha campanha, string imagemBase64, string caminho, out object dadosRetorno);
    }
}
