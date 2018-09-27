using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Services.Api.Identity;
using SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais;
using SantaHelena.ClickDoBem.Services.Api.Model.Response.Credenciais;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers.Credenciais
{

    /// <summary>
    /// API de Usuario
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/usuario")]
    [Authorize]
    public class UsuarioController : CdbApiControllerBase
    {

        #region Objetos/Variáveis Locais

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        protected readonly IUsuarioAppService _appService;
        protected readonly IHostingEnvironment _hostingEnvironment;
        protected readonly UserManager<ApiAppUser> _userManager;
        protected readonly JwtTokenOptions _jwtTokenOptions;

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância da API
        /// </summary>
        public UsuarioController
        (
            IUsuarioAppService appService,
            IHostingEnvironment hostingEnvironment,
            //UserManager<ApiAppUser> userManager,
            IOptions<JwtTokenOptions> jwtTokenOptions
        )
        {
            //TODO: Migrar de IdentityUser para UserCustom
            //_userManager = userManager;
            _jwtTokenOptions = jwtTokenOptions.Value;
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Converte data em milisegundos
        /// </summary>
        /// <param name="date">Data a ser convertida</param>
        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

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
        [AllowAnonymous]
        public IActionResult Autenticar([FromBody] AutenticacaoRequest request)
        {

            string token = null;
            string mensagem = null;
            DateTime? validade = null;
            int statusCode = StatusCodes.Status403Forbidden;

            bool autenticado = _appService.Autenticar(request.Nome, request.Senha, out mensagem);

            //ApiAppUser user = _userManager.FindByNameAsync(request.Nome).Result;

            if (autenticado)
            {
                var jwt = new JwtSecurityToken(
                      issuer: _jwtTokenOptions.Issuer,
                      audience: _jwtTokenOptions.Audience,
                      notBefore: _jwtTokenOptions.NotBefore,
                      expires: _jwtTokenOptions.Expiration,
                      signingCredentials: _jwtTokenOptions.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                token = encodedJwt;
                validade = DateTime.Now.AddMilliseconds((int)_jwtTokenOptions.ValidFor.TotalSeconds);
            }
            return StatusCode(statusCode, new AutenticacaoResponse(autenticado, mensagem, token, validade));
        }

        /// <summary>
        /// Listar todos os usuários
        /// </summary>
        /// <response code="200">Retorna lista de usuários</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            return Ok(_appService.ObterTodos());
        }


        #endregion

    }
}