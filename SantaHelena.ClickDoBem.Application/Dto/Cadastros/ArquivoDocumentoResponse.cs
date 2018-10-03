using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Dto.Cadastros
{

    /// <summary>
    /// Modelo de resposta do processamento de arquivo de documento
    /// </summary>
    public class ArquivoDocumentoDto
    {

        /// <summary>
        /// Cria uma nova instância do Dto
        /// </summary>
        public ArquivoDocumentoDto()
        {
            Linhas = new List<LinhaArquivoDocumentoDto>();
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
        /// Detalhe do processamento do arquivo
        /// </summary>
        public string Detalhe { get; set; }

        /// <summary>
        /// Detalhes das linhas processadas
        /// </summary>
        public IList<LinhaArquivoDocumentoDto> Linhas { get; set; }

    }
}
