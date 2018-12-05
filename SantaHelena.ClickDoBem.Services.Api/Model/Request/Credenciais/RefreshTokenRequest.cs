using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais
{

    /// <summary>
    /// Request de Refresh de Autenticação (Token)
    /// </summary>
    public class RefreshTokenRequest
    {

        /// <summary>
        /// Token expirado para renovação
        /// </summary>
        [Required(ErrorMessage = "O token deve ser informado")]
        public string Token { get; set; }

    }

}
