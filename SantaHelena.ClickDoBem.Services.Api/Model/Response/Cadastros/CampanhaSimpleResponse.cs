using System;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Cadastros
{

    /// <summary>
    /// Objeto de resposta de Api de Campanha
    /// </summary>
    public class CampanhaSimpleResponse
    {

        /// <summary>
        /// Id do Campanha
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descrição do Campanha
        /// </summary>
        public string Descricao { get; set; }

    }
}
