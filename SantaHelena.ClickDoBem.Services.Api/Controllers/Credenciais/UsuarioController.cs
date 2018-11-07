using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
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
        protected readonly IAppUser _usuario;
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
            IOptions<JwtTokenOptions> jwtTokenOptions,
            IAppUser usuario
        )
        {
            _jwtTokenOptions = jwtTokenOptions.Value;
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
            _usuario = usuario;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Propriedade de configuração
        /// </summary>
        public IConfigurationRoot Configuration { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Gerar token
        /// </summary>
        /// <returns>string contendo o token</returns>
        protected string GeraToken(UsuarioDto usuarioDto, IList<string> perfis, out DateTime? validade)
        {
            IList<Claim> claimnsUsuario = new List<Claim>();

            claimnsUsuario.Add(new Claim(ClaimTypes.Hash, usuarioDto.Id.ToString()));
            claimnsUsuario.Add(new Claim(ClaimTypes.Surname, usuarioDto.UsuarioLogin.Login));
            claimnsUsuario.Add(new Claim(ClaimTypes.Name, usuarioDto.Nome));

            foreach (string p in usuarioDto.UsuarioPerfil)
            {
                claimnsUsuario.Add(new Claim(ClaimTypes.Role, p));
                perfis.Add(p);
            }

            JwtSecurityToken jwt = new JwtSecurityToken(
                 issuer: _jwtTokenOptions.Issuer,
                 audience: _jwtTokenOptions.Audience,
                 claims: claimnsUsuario,
                 notBefore: _jwtTokenOptions.NotBefore,
                 expires: _jwtTokenOptions.Expiration,
                 signingCredentials: _jwtTokenOptions.SigningCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            validade = jwt.ValidTo;

            return token;

        }

        /// <summary>
        /// Obter o cpf do token
        /// </summary>
        /// <param name="token">token enviado</param>
        /// <param name="cpf">Variável de saída do cpf</param>
        /// <param name="validade">Variável de saída da data de validade do token</param>
        protected void ObterCpfToken(string token, out string cpf, out DateTime validade)
        {

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken tokenSecure = handler.ReadToken(token) as SecurityToken;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var key = Encoding.ASCII.GetBytes(Configuration["JwtTokenSecurity:SecretKey"]);

            TokenValidationParameters validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            ClaimsPrincipal claims = handler.ValidateToken(token, validations, out tokenSecure);
            Claim surname = claims.Claims.Where(c => c.Type.Equals(ClaimTypes.Surname)).FirstOrDefault();
            cpf = surname.Value.ToString();
            validade = tokenSecure.ValidTo;

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
        ///         "sucesso" : "true",
        ///         "mensagem" : "mensagem do resultado da operação",
        ///         "token" : "hash informado se sucesso, caso contrário será nulo",
        ///         "dataValidade : "data de validade do token quando sucesso, em caso de falha será informado nulo",
        ///         "perfis" : [
        ///             "Admin",
        ///             "Colaborador"
        ///         ],
        ///         "usuario": {
        ///             "id": "guid",
        ///             "dataInclusao": "2018-10-08T12:18:36",
        ///             "dataAlteracao": null,
        ///             "cpfCnpj": "00000000000",
        ///             "nome": "string",
        ///             "usuarioLogin": {
        ///                 "login": "string",
        ///                 "senha": "*ENCRYPTED*"
        ///             },
        ///             "usuarioDados": {
        ///                 "id": "guid",
        ///                 "dataInclusao": "2018-10-08T12:18:36",
        ///                 "dataAlteracao": null,
        ///                 "dataNascimento": "AAAA-MM-DD",
        ///                 "logradouro": "string",
        ///                 "numero": "string",
        ///                 "complemento": "string",
        ///                 "bairro": "string",
        ///                 "cidade": "string",
        ///                 "uf": "string",
        ///                 "cep": "string",
        ///                 "telefoneCelular": "string",
        ///                 "telefoneFixo": "string",
        ///                 "email": "email@provedor"
        ///             },
        ///             "usuarioPerfil": [
        ///                 "Colaborador"
        ///             ]
        ///         }
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
            DateTime? validade;
            int statusCode = StatusCodes.Status403Forbidden;
            IList<string> perfis = new List<string>();

            bool autenticado = _appService.Autenticar(request.Nome, request.Senha, out string mensagem, out UsuarioDto usuarioDto);

            if (autenticado)
            {

                statusCode = StatusCodes.Status200OK;

                token = GeraToken(usuarioDto, perfis, out validade);

            }
            else
            {
                validade = null;
            }
            return StatusCode(statusCode, new AutenticacaoResponse(autenticado, mensagem, token, validade, perfis, usuarioDto));
        }
        

        /// <summary>
        /// Realizar a recuperação da senha do colaborador
        /// </summary>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição
        ///     {
        ///         "cpfCnpj": "string",
        ///         "dataNascimento": "AAAA-MM-DD",
        ///         "novaSenha": "string",
        ///         "confirmarSenha": "string"
        ///     }
        ///     
        ///     Resposta
        ///     {
        ///         "sucesso" : boolean,
        ///         "mensagem": "texto com o resultado da operação"
        ///     }
        ///     
        /// </remarks>
        [HttpPost("esquecisenha")]
        [AllowAnonymous]
        public IActionResult EsqueciSenha([FromBody] EsqueciSenhaRequest request)
        {
            if (!ModelState.IsValid)
                return Response<EsqueciSenhaRequest>(request);

            bool sucesso = _appService.EsqueciSenha(request.CpfCnpj, request.DataNascimento, request.NovaSenha, request.ConfirmarSenha, out int statusCode, out string mensagem);
            return StatusCode(statusCode, new { Sucesso = sucesso, Mensagem = mensagem });
        }

        /// <summary>
        /// Realizar a troca de senha do colaborador
        /// </summary>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição
        ///     {
        ///         "documento": "string",
        ///         "senhaAtual": "hashMD5",
        ///         "novaSenha": "string",
        ///         "confirmarSenha": "string"
        ///     }
        ///     
        ///     Resposta
        ///     {
        ///         "sucesso": boolean,
        ///         "mensagem": "texto com o resultado da operação"
        ///     }
        /// 
        /// </remarks>
        [HttpPost("trocarsenha")]
        public IActionResult AlterarSenha([FromBody] TrocaSenhaRequest request)
        {

            if (!ModelState.IsValid)
                return Response<TrocaSenhaRequest>(request);

            bool sucesso = _appService.TrocarSenha(_usuario, request.SenhaAtual, request.NovaSenha, request.ConfirmarSenha, out int statusCode, out string mensagem);
            return StatusCode(statusCode, new { Sucesso = sucesso, Mensagem = mensagem });


        }

        /// <summary>
        /// Realizar a atualização (refresh) de um token expirado
        /// </summary>
        /// <remarks>
        /// Contrato
        ///
        ///     Requisição
        ///     {
        ///        "token": "string",
        ///     }
        ///     
        ///     Resposta
        ///     { 
        ///         "sucesso" : "true",
        ///         "mensagem" : {
        ///             "token": "string"
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <param name="request">Informações da requisição</param>
        /// <response code="200">Sucesso na geraçãod de novo token</response>
        /// <response code="403">Falha na autenticação/Acesso Negado</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public IActionResult AtualizarToken([FromBody]RefreshTokenRequest request)
        {

            int statusCode = StatusCodes.Status403Forbidden;
            DateTime? validade;
            IList<string> perfis = new List<string>();
            string token = null;

            ObterCpfToken(request.Token, out string cpf, out DateTime validadeTokenAntigo);
            if (validadeTokenAntigo.AddDays(10) < DateTime.UtcNow)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    Sucesso = false,
                    Mensagem = "Data de renovação de token expirada"
                });
            }

            bool autenticado = _appService.Autenticar(cpf, out string mensagem, out UsuarioDto usuarioDto);
            if (usuarioDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Sucesso = false,
                    Mensagem = mensagem
                });
            }

            if (autenticado)
            {
                statusCode = StatusCodes.Status200OK;
                token = GeraToken(usuarioDto, perfis, out validade);
            }
            else
            {
                validade = null;
            }
            return StatusCode(statusCode, new AutenticacaoResponse(autenticado, mensagem, token, validade, perfis, usuarioDto));

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
        ///                 "Admin"
        ///             ]
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <returns>Lista dos registros cadastrados</returns>
        /// <response code="200">Retorna a lista de registros cadastrados</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet]
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
        ///         "situacao": "ativo",
        ///         "cadastroCompleto": true
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
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet("verificadocumento/{documento}")]
        [AllowAnonymous]
        public IActionResult VerificarDocumentoHabilitado(string documento)
        {
            _appService.VerificarSituacaoDocumento(documento, out string situacao, out bool cadastrado);
            return Ok(new { situacao, cadastrado });
        }

        #endregion

    }
}
