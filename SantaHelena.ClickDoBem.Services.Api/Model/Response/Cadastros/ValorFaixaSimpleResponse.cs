using System;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Cadastros
{

    /// <summary>
    /// Objeto de resposta de Api de ValorFaixa
    /// </summary>
    public class ValorFaixaSimpleResponse
    {

        /// <summary>
        /// Id do ValorFaixa
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descrição do ValorFaixa
        /// </summary>
        public string Descricao { get; set; }

    }
}
