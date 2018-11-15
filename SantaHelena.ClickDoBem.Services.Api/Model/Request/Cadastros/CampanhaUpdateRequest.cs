using System;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de UPDATE de Campanha
    /// </summary>
    public class CampanhaUpdateRequest : CampanhaInsertRequest
    {

        /// <summary>
        /// Id do registro
        /// </summary>
        [Key]
        public Guid Id { get; set; }

    }

}
