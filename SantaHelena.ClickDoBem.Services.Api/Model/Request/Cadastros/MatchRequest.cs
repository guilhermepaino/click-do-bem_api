using System;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Modelo de request de match
    /// </summary>
    public class MatchRequest
    {

        /// <summary>
        /// Id de doação
        /// </summary>
        [Required(ErrorMessage = "O id do item de doação deve ser informado")]
        public Guid? DoacaoId { get; set; }

        /// <summary>
        /// Id de necessidade
        /// </summary>
        [Required(ErrorMessage = "O id do item de necessidade deve ser informado")]
        public Guid? NecessidadeId { get; set; }

    }
}
