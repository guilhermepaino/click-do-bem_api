using System;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de Upload de Imagens
    /// </summary>
    public class ItemImagemRequest : SimpleImagemRequest
    {

        /// <summary>
        /// Id do item
        /// </summary>
        public Guid ItemId { get; set; }

    }

}
