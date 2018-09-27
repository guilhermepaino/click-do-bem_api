using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais
{

    /// <summary>
    /// Request de Autenticacao
    /// </summary>
    public class AutenticacaoRequest
    {

        /// <summary>
        /// Nome do usuário (UserName)
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Senha do usuário (Hash Md5)
        /// </summary>
        public string Senha { get; set; }

    }
}
