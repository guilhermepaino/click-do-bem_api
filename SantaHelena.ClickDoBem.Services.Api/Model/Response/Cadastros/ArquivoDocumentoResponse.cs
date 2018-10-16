using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Cadastros
{

    /// <summary>
    /// Modelo de resposta do processamento de arquivo de documento
    /// </summary>
    public class ArquivoDocumentoResponse
    {

        /// <summary>
        /// Cria uma nova instância da response
        /// </summary>
        public ArquivoDocumentoResponse()
        {
            Linhas = new List<LinhaArquivoDocumentoResponse>();
        }

        /// <summary>
        /// Nome do arquivo
        /// </summary>
        public string NomeArquivo { get; set; }

        /// <summary>
        /// Flag indicando o resultado do processamento do arquivo
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Detalhe geral do arquivo
        /// </summary>
        public string Detalhe { get; set; }

        /// <summary>
        /// Detalhes das linhas processadas
        /// </summary>
        public IList<LinhaArquivoDocumentoResponse> Linhas { get; set; }

    }
}
