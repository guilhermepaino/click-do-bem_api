using System;
using System.Collections.Generic;

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
        /// <param name="perfis">Lista de perfis</param>
        public AutenticacaoResponse(bool sucesso, string mensagem, string token, DateTime? dataValidade, IEnumerable<string> perfis)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Token = token;
            DataValidade = dataValidade;
            Perfis = perfis;
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

        /// <summary>
        /// Lista de perfis
        /// </summary>
        public IEnumerable<string> Perfis { get; set; }

        #endregion

    }
}
