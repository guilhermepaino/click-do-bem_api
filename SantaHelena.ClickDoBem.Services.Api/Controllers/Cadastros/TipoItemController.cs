using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers.Cadastros
{

    /// <summary>
    /// API de TipoItem
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/TipoItem")]
    public class TipoItemController : CdbApiControllerBase
    {

        #region Objetos/Variáveis Locais

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        protected readonly ITipoItemAppService _appService;
        protected readonly IHostingEnvironment _hostingEnvironment;

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância da API
        /// </summary>
        public TipoItemController
        (
            ITipoItemAppService appService,
            IHostingEnvironment hostingEnvironment
        )
        {
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Métodos/EndPoints Api

        /// <summary>
        /// Listar todos os registros
        /// </summary>
        /// <remarks>
        /// Contrato
        ///
        ///     Requisição
        ///     Nenhum parâmetro
        ///     
        ///     Resposta (array)
        ///     [
        ///         {
        ///             "id": "0acd2b81-c5a5-11e8-ab80-0242ac110006",
        ///             "dataInclusao": "2018-09-30T19:04:19",
        ///             "dataAlteracao": null,
        ///             "descricao": "Necessidade",
        ///         },
        ///         {
        ///             "id": "0acd2bb5-c5a5-11e8-ab80-0242ac110006",
        ///             "dataInclusao": "2018-09-30T19:04:21",
        ///             "dataAlteracao": null,
        ///             "descricao": "Doação"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <returns>Lista dos registros cadastrados</returns>
        /// <response code="200">Retorna a lista de registros cadastrados</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_appService.ObterTodos());
        }

        #endregion

    }
}