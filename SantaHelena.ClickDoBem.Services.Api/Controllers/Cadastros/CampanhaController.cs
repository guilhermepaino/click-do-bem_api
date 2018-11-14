using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using System;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers.Cadastros
{

    /// <summary>
    /// API de Campanha
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/Campanha")]
    public class CampanhaController : CdbApiControllerBase
    {

        #region Objetos/Variáveis Locais

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        protected readonly ICampanhaAppService _appService;
        protected readonly IHostingEnvironment _hostingEnvironment;

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância da API
        /// </summary>
        public CampanhaController
        (
            ICampanhaAppService appService,
            IHostingEnvironment hostingEnvironment
        )
        {
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Métodos/EndPoints Api

        /// <summary>
        /// Listar todas as campanhas ativas
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
        ///             "id": "guid",
        ///             "dataInclusao": "0001-01-01T00:00:00",
        ///             "dataAlteracao": "0001-01-01T00:00:00",
        ///             "descricao": "string",
        ///             "dataInicial": "0001-01-01T00:00:00",
        ///             "dataFinal": "0001-01-01T00:00:00"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <returns>Lista dos registros de campanhas ativas</returns>
        /// <response code="200">Retorna a lista de registros cadastrados</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet]
        public IActionResult ListarAtivas()
        {
            return Ok(_appService.ObterAtivas());
        }

        /// <summary>
        /// Listar todas as campanhas ativas, futuras e expiradas
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
        ///             "id": "guid",
        ///             "dataInclusao": "0001-01-01T00:00:00",
        ///             "dataAlteracao": "0001-01-01T00:00:00",
        ///             "descricao": "string",
        ///             "dataInicial": "0001-01-01T00:00:00",
        ///             "dataFinal": "0001-01-01T00:00:00"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <returns>Lista dos registros de campanhas ativas</returns>
        /// <response code="200">Retorna a lista de registros cadastrados</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet("todas")]
        public IActionResult ListarTodas()
        {
            return Ok(_appService.ObterTodos());
        }

        /// <summary>
        /// Localizar uma campanha pelo id
        /// </summary>
        /// <remarks>
        /// Contrato
        ///
        ///     Requisição
        ///     url: [URI]/api/versao/campanha/guid
        ///     
        ///     Resposta (array)
        ///     {
        ///         "id": "guid",
        ///         "dataInclusao": "0001-01-01T00:00:00",
        ///         "dataAlteracao": "0001-01-01T00:00:00",
        ///         "descricao": "string",
        ///         "dataInicial": "0001-01-01T00:00:00",
        ///         "dataFinal": "0001-01-01T00:00:00"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Lista dos registros de campanhas ativas</returns>
        /// <response code="200">Retorna a lista de registros cadastrados</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet("{id:guid}")]
        public IActionResult LocalizarPorId(Guid id)
        {
            return Ok(_appService.ObterPorId(id));
        }

        #endregion

    }
}