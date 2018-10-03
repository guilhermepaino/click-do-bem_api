using SantaHelena.ClickDoBem.Domain.Core.Enums;

namespace SantaHelena.ClickDoBem.Application.Dto.Cadastros
{

    /// <summary>
    /// Modelo de resposta da linha de arquivo de upload de documento
    /// </summary>
    public class LinhaArquivoDocumentoDto
    {

        /// <summary>
        /// Número da linha
        /// </summary>
        public int Linha { get; set; }

        /// <summary>
        /// Flag indicando o resultado do processamento
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Detalhe do processamento
        /// </summary>
        public string Detalhe { get; set; }

        /// <summary>
        /// Acao do registro
        /// </summary>
        public AcaoDocumento Acao { get; set; }

    }

}
