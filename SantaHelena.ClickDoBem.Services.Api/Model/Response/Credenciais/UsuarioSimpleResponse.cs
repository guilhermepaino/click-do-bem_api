using System;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Credenciais
{

    /// <summary>
    /// Objeto de response de usuario, modelo simples
    /// </summary>
    public class UsuarioSimpleResponse
    {

        /// <summary>
        /// Id do usuário
        /// </summary>
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string CpfCnpj { get; set; }

    }
}
