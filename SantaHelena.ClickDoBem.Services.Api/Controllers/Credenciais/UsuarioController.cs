﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        /// Autenticar usuário backoffice
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

            bool autenticado = _appService.Autenticar(request.Nome, request.Senha, out string mensagem);

            if (autenticado)
            {
                var jwt = new JwtSecurityToken(
                      issuer: _jwtTokenOptions.Issuer,
                      audience: _jwtTokenOptions.Audience,
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
        ///     Nenhum parâmetro
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
        ///             }
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <returns>Lista dos registros cadastrados</returns>
        /// <response code="200">Retorna a lista de registros cadastrados</response>
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            return Ok(_appService.ObterTodos());
        }

        /// <summary>
        /// Recepciona arquivo de carga de colaboradores
        /// </summary>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição
        ///     Content-Type: multipart/form-data
        ///     Enviar arquivo através de FormFile
        ///     
        ///     Resposta
        ///     {
        ///         "sucesso" : "boolean",
        ///         "mensagem" : "mensagem do resultado do processamento",
        ///         "resultado" : 
        ///         [
        ///             { 
        ///                 "linha": "1",
        ///                 "situacao": "Ok"
        ///             },
        ///             { 
        ///                "linha": "2",
        ///               "situacao": "Cpf/Cnpj inválido"
        ///             }
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Processamento do arquivo realizado com sucesso</response>
        /// <response code="400">Falha na requisição (arquivo inválido ou tamanho zero)</response>
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Ocorreu alguma falha no processamento da request</response>
        [HttpPost("upload"), DisableRequestSizeLimit]
        [AllowAnonymous]
        public IActionResult Upload()
        {
            //TODO: AllowAnonymous

            IFormFile file = null;

            file = Request.Form.Files.FirstOrDefault();

            //try { file = Request.Form.Files.FirstOrDefault(); }
            //catch { return BadRequest(new { sucesso = false, mensagem = "Nenhum arquivo foi enviado" }); }

            //if (file.Length.Equals(0))
            //    return BadRequest(new { sucesso = false, mensagem = "Arquivo enviado é inválido (tamanho zero)!" });

            return Ok();

            //string protocolo = Request.IsHttps ? "https" : "http";
            //string urlApp = $"{protocolo}://{Request.Host.Value}";
            //string token = Request.Headers.ToList().Where(x => x.Key.Equals("Authorization")).SingleOrDefault().Value.ToString();

            //bool sucess = _importacaoMetaAppService.Upload(file, urlApp, token, _caminho, ano, mes, out int statusCode, out string message);

            //if (sucess)
            //    return Ok(new { sucess = true, data = $"Arquivo '{file.FileName} ({file.Length.ToString("N0")} bytes)' adicionado a fila com sucesso!" });
            //else
            //    return StatusCode(statusCode, new { sucess = false, data = $"Falha no envio do arquivo: {message}" });
        }

        #endregion

    }
}