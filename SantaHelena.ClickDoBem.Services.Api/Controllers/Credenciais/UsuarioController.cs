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
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
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
    public class UsuarioController : CdbApiControllerBase
    {

        #region Objetos/Variáveis Locais

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        protected readonly IUsuarioAppService _appService;
        protected readonly IHostingEnvironment _hostingEnvironment;
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
            IOptions<JwtTokenOptions> jwtTokenOptions
        )
        {
            _jwtTokenOptions = jwtTokenOptions.Value;
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Métodos/EndPoints Api

        /// <summary>
        /// Autenticar usuário no sistema / gera chave (token) de acesso
        /// </summary>
        /// <remarks>
        /// Contrato
        ///
        ///     Requisição
        ///     {
        ///        "nome": "admin",
        ///        "senha": "202cb962ac59075b964b07152d234b70",
        ///     }
        ///     
        ///     Resposta
        ///     { 
        ///         "sucesso" = "true",
        ///         "mensagem" = "mensagem do resultado da operação",
        ///         "token" = "hash informado se sucesso, caso contrário será nulo",
        ///         "dataValidade = "data de validade do token quando sucesso, em caso de falha será informado nulo"
        ///     }
        ///     
        /// </remarks>
        /// <param name="request">Informações da requisição</param>
        /// <response code="200">Sucesso na autenticação</response>
        /// <response code="403">Falha na autenticação/Acesso Negado</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPost("autenticar")]
        [AllowAnonymous]
        public IActionResult Autenticar([FromBody] AutenticacaoRequest request)
        {

            string token = null;
            DateTime? validade = null;
            int statusCode = StatusCodes.Status403Forbidden;

            bool autenticado = _appService.Autenticar(request.Nome, request.Senha, out string mensagem, out UsuarioDto usuarioDto);

            if (autenticado)
            {

                statusCode = StatusCodes.Status200OK;
		    
		IList<Claim> claimnsUsuario = new List<Claim>();

                claimnsUsuario.Add(new Claim(ClaimTypes.Hash, usuarioDto.Id.ToString()));
                claimnsUsuario.Add(new Claim(ClaimTypes.Surname, usuarioDto.UsuarioLogin.Login));
                claimnsUsuario.Add(new Claim(ClaimTypes.Name, usuarioDto.Nome));

                foreach (UsuarioPerfilDto p in usuarioDto.UsuarioPerfil)
                    claimnsUsuario.Add(new Claim(ClaimTypes.Role, p.Perfil));

                JwtSecurityToken jwt = new JwtSecurityToken(
                     issuer: _jwtTokenOptions.Issuer,
                     audience: _jwtTokenOptions.Audience,
                     claims: claimnsUsuario,
                     notBefore: _jwtTokenOptions.NotBefore,
                     expires: _jwtTokenOptions.Expiration,
                     signingCredentials: _jwtTokenOptions.SigningCredentials);

                token = new JwtSecurityTokenHandler().WriteToken(jwt);
                validade = DateTime.Now.AddMilliseconds((int)_jwtTokenOptions.ValidFor.TotalSeconds);
            }
            return StatusCode(statusCode, new AutenticacaoResponse(autenticado, mensagem, token, validade));
        }

        /// <summary>
        /// Listar todos os registros
        /// </summary>
        /// <remarks>
        /// Contrato
        ///
        ///     Requisição
        ///     Parâmetro: Perfil -> Filtra somente usuários do perfil informado (opcional)
        ///     
        ///     Resposta (array)
        ///     [
        ///         {
        ///             "id": "80f763b0-c327-11e8-bbe3-0242ac110006",
        ///             "dataInclusao": "2018-09-28T14:05:06",
        ///             "dataAlteracao": null,
        ///             "cpfCnpj": "11111111111",
        ///             "nome": "admin",
        ///             "usuarioLogin": {
        ///                 "login": "admin",
        ///                 "senha": "*ENCRYPTED*"
        ///             },
        ///             "usuarioDados": {
        ///                 "id": "f381eaf4-c37e-11e8-8987-0242ac110006",
        ///                 "dataInclusao": "2018-09-28T00:31:04",
        ///                 "dataAlteracao": null,
        ///                 "dataNascimento": "1976-11-13T00:00:00",
        ///                 "logradouro": "string",
        ///                 "numero": "string",
        ///                 "complemento": "string",
        ///                 "bairro": "string",
        ///                 "cidade": "string",
        ///                 "uf": "string",
        ///                 "cep": "00000000",
        ///                 "telefoneCelular": "(00)00000-0000",
        ///                 "telefoneFixo": ""(00)0000-0000",
        ///                 "email": "email@provedor"
        ///             },
        ///             "usuarioPerfil": [
        ///                 {
        ///                     "perfil": "Admin"
        ///                 }
        ///             ]
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
            IEnumerable<UsuarioDto> result;
            if (string.IsNullOrWhiteSpace(perfil))
                result = _appService.ObterTodos();
            else
                result = _appService.ObterPorPerfil(perfil);
            return Ok(result);
        }

        /// <summary>
        /// Verifica o status de um documento
        /// </summary>
        /// <remarks>
        /// Contrato
        ///
        ///     Requisição
        ///     url: [URI]/api/versao/usuario/verificadocumento/11111111111
        ///     
        ///     Resposta (array)
        ///     {
        ///         "situacao": "ativo"
        ///     }
        ///     
        ///     situações possíveis: 
        ///     
        ///         ativo           = cadastrado e habilitado para fazer cadastro
        ///         inativo         = cadastrado e INABILITADO para fazer cdastro
        ///         inexistente     = não cadastrado
        ///     
        /// </remarks>
        /// <returns>Situação de cadastro do documento</returns>
        /// <response code="200">Sucesso na busca</response>
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet("verificadocumento/{documento}")]
        [AllowAnonymous]
        public IActionResult VerificarDocumentoHabilitado(string documento)
        {
            return Ok(new { situacao = _appService.VerificarSituacaoDocumento(documento) });
        }

        #endregion

    }
}
