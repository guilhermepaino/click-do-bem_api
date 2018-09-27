using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais;
using SantaHelena.ClickDoBem.Services.Api.Model.Response.Credenciais;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers.Credenciais
{

    /// <summary>
    /// API de Usuario
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/usuario")]
    public class UsuarioController : CdbApiControllerBase
    {

        #region Objetos/Variáveis Locais

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        protected readonly IUsuarioAppService _appService;
        protected readonly IHostingEnvironment _hostingEnvironment;

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância da API
        /// </summary>
        public UsuarioController
        (
            IUsuarioAppService appService,
            IHostingEnvironment hostingEnvironment
        )
        {
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Métodos/EndPoints Api

        /// <summary>
        /// Autenticar usuário backoffice
        /// </summary>
        /// <param name="request">Informações da requisição</param>
        /// <response code="200">Sucesso na autenticação</response>
        /// <response code="403">Falha na autenticação/Acesso Negado</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPost]
        [Route("autenticar")]
        public IActionResult Autenticar([FromBody]AutenticacaoRequest request)
        {
            return Ok(new AutenticacaoResponse(true, "Autenticado com sucesso", Guid.NewGuid().ToString().Replace("-", ""), DateTime.Now.AddHours(8)));
            //TODO: Implementar - Ainda está Mockado
        }

        #endregion

    }
}