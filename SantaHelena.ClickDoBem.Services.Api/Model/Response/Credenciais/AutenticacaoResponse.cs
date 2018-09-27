using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Credenciais
{

    /// <summary>
    /// Response de Autenticacao
    /// </summary>
    public class AutenticacaoResponse
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instâncioa da resposta de autenticação
        /// </summary>
        /// <param name="sucesso">Status da autenticação</param>
        /// <param name="mensagem">Mensagem da autenticação</param>
        /// <param name="token">Token gerado</param>
        /// <param name="dataValidade">Data de validade do token</param>
        public AutenticacaoResponse(bool sucesso, string mensagem, string token, DateTime? dataValidade)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Token = token;
            DataValidade = dataValidade;
        }

        #endregion


        #region Propriedades

        /// <summary>
        /// Status da autenticação
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Mensagem da autenticação
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Token gerado em caso de sucesso na autenticação
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Data de validade do token
        /// </summary>
        public DateTime? DataValidade { get; set; }

        #endregion

    }
}
