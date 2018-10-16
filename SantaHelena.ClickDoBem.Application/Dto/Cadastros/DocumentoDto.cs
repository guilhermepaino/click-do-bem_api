using SantaHelena.ClickDoBem.Domain.Core.Enums;

namespace SantaHelena.ClickDoBem.Application.Dto.Cadastros
{

    /// <summary>
    /// Dto de documento
    /// </summary>
    internal class DocumentoDto
    {

        /// <summary>
        /// Número do documento Cpf/Cnpj
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Situação do cadastro
        /// </summary>
        public string Situacao { get; set; }

        /// <summary>
        /// Acao a ser tomada com o documento
        /// </summary>
        public AcaoDocumento Acao { get; set; }

    }
}
