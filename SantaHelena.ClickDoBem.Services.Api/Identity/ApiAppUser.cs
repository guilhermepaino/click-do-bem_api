using Microsoft.AspNetCore.Identity;

namespace SantaHelena.ClickDoBem.Services.Api.Identity
{

    /// <summary>
    /// Objeto de usuário da API da aplicação
    /// </summary>
    public class ApiAppUser : IdentityUser
    {

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }

    }

}