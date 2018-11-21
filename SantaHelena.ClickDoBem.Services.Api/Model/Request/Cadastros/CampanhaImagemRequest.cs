using System;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de upload de imagens de campanha
    /// </summary>
    public class CampanhaImagemRequest
    {

        /// <summary>
        /// Id da campanha
        /// </summary>
        public Guid CampanhaId { get; set; }

        /// <summary>
        /// Expressão string do arquivo (Base64)
        /// </summary>
        public string ImagemBase64 { get; set; }

    }
}
