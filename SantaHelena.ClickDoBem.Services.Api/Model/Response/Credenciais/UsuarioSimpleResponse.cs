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

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Documento do usuário
        /// </summary>
        public string CpfCnpj { get; set; }

    }
}
