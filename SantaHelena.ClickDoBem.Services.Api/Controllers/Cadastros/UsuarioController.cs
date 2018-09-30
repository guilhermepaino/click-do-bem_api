using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Services.Api.Identity;
using SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais;
using SantaHelena.ClickDoBem.Services.Api.Model.Response.Credenciais;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers.Cadastros
{

    /// <summary>
    /// API de Categoria
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/Categoria")]
    public class CategoriaController : CdbApiControllerBase
    {

        #region Objetos/Variáveis Locais

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        protected readonly ICategoriaAppService _appService;
        protected readonly IHostingEnvironment _hostingEnvironment;

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância da API
        /// </summary>
        public CategoriaController
        (
            ICategoriaAppService appService,
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
        ///             "id": "2ef307a6-c4a5-11e8-8776-0242ac110006",
        ///             "dataInclusao": "2018-09-30T19:04:19",
        ///             "dataAlteracao": "0001-01-01T00:00:00",
        ///             "descricao": "Higiene e limpeza",
        ///             "pontuacao": 10,
        ///             "gerenciadaRh": false
        ///         },
        ///         {
        ///             "id": "340c1a33-c4a5-11e8-8776-0242ac110006",
        ///             "dataInclusao": "2018-09-30T19:04:21",
        ///             "dataAlteracao": "0001-01-01T00:00:00",
        ///             "descricao": "Bebê",
        ///             "pontuacao": 100,
        ///             "gerenciadaRh": false
        ///         },
        ///         {
        ///             "id": "38f292cc-c4a5-11e8-8776-0242ac110006",
        ///             "dataInclusao": "2018-09-30T19:04:23",
        ///             "dataAlteracao": "0001-01-01T00:00:00",
        ///             "descricao": "Telefonia e acessórios",
        ///             "pontuacao": 10,
        ///             "gerenciadaRh": true
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <returns>Lista dos registros cadastrados</returns>
        /// <response code="200">Retorna a lista de registros cadastrados</response>
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet("listar")]
        public IActionResult Listar([FromQuery]string perfil)
        {
            return Ok(_appService.ObterTodos());
        }

        #endregion

    }
}