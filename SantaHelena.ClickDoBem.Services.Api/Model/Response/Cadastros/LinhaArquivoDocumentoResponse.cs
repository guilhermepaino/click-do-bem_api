namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Cadastros
{

    /// <summary>
    /// Modelo de resposta da linha de arquivo de upload de documento
    /// </summary>
    public class LinhaArquivoDocumentoResponse
    {

        /// <summary>
        /// Número da linha
        /// </summary>
        public int Linha { get; set; }

        /// <summary>
        /// Conteudo da Linha
        /// </summary>
        public string Conteudo { get; set; }

        /// <summary>
        /// Flag indicando o resultado do processamento
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Detalhe do processamento
        /// </summary>
        public string Detalhe { get; set; }

    }
}
