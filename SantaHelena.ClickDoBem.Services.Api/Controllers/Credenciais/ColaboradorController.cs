using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers.Credenciais
{

    /// <summary>
    /// API de Colaborador
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/colaborador")]
    public class ColaboradorController : CdbApiControllerBase
    {

        #region Objetos/Variáveis Locais

        protected readonly IColaboradorAppService _appService;
        protected readonly IHostingEnvironment _hostingEnvironment;

        #endregion

        #region Construtores

        public ColaboradorController
        (
            IColaboradorAppService appService,
            IHostingEnvironment hostingEnvironment
        )
        {
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        /// <summary>
        /// Listar todos os Cpfs cadastrados
        /// </summary>
        /// <returns>Lista dos cpfs cadastrados</returns>
        /// <response code="200">Retorna a lista de cpfs cadastrados</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            return Ok(_appService.ObterTodos());
        }

    }
}