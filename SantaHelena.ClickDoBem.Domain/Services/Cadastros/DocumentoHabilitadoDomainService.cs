using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class DocumentoHabilitadoDomainService : DomainServiceBase<DocumentoHabilitado, IDocumentoHabilitadoRepository>, IDocumentoHabilitadoDomainService
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public DocumentoHabilitadoDomainService(IDocumentoHabilitadoRepository repository) : base(repository) { }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Obter registro pelo número do documento (cpf/cnpj)
        /// </summary>
        /// <param name="doc">Número do documento cpf ou cnpj</param>
        public DocumentoHabilitado ObterPorDocumento(string cpfCnpj) => _repository.ObterPorDocumento(cpfCnpj);

        #endregion

    }

}