using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Security;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers.Cadastros
{

    /// <summary>
    /// API de Usuario
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/colaborador")]
    public class ColaboradorController : CdbApiControllerBase
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
        public ColaboradorController
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
        /// Cadastrar um novo colaborador
        /// </summary>
        /// <remarks>
        /// 
        /// Contrato
        /// 
        ///     Requisição
        ///     ColaboradorInsertRequest
        ///     
        ///     Exemplo:
        ///     {
        ///         "documento": "43994645000",
        ///         "nome": "JOAO DA SILVA",
        ///         "dataNascimento": "1976-11-13",
        ///         "endereco": {
        ///             "logradouro": "RUA DOS BOBOS",
        ///             "numero": "0",
        ///             "complemento": "BLOCO A - APTO 00",
        ///             "bairro": "PARQUE DOS DESORIENTADOS",
        ///             "cidade": "ARARAQUARA",
        ///             "uf": "SP",
        ///             "cep": "16123789"
        ///         },
        ///         "telefoneFixo": "",
        ///         "telefoneCelular": "(16)91234-1234",
        ///         "email": "usuario.teste@s2it.com.br",
        ///         "senha": "a1b2c3d4"
        ///     }
        /// 
        /// </remarks>
        /// <param name="req">Modelo de requisição de cadastro de usuário</param>
        /// <response code="200">Cadastro realizado com sucesso</response>
        /// <response code="400">Falha na requisição, detalhes na mensagem (exemplo: Usuário já cadastrado)</response>
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="404">Registro de pré-cadastro não encontrado, detalhes no campo mensagem</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPost("cadastrar")]
        [AllowAnonymous]
        public IActionResult Cadastrar([FromBody]ColaboradorInsertRequest req)
        {

            if (!ModelState.IsValid)
                return Response<ColaboradorInsertRequest>(req);

            UsuarioDto dto = new UsuarioDto()
            {
                CpfCnpj = req.Documento,
                Nome = req.Nome,
                UsuarioDados = new UsuarioDadosDto()
                {
                    DataNascimento = req.DataNascimento,
                    Logradouro = req.Endereco.Logradouro,
                    Numero = req.Endereco.Numero,
                    Complemento = req.Endereco.Complemento,
                    Bairro = req.Endereco.Bairro,
                    Cidade = req.Endereco.Cidade,
                    UF = req.Endereco.Uf,
                    CEP = req.Endereco.Cep,
                    TelefoneFixo = req.TelefoneFixo,
                    TelefoneCelular = req.TelefoneCelular,
                    Email = req.Email
                },
                UsuarioLogin = new UsuarioLoginDto()
                {
                    Login = req.Documento,
                    Senha = MD5.ByteArrayToString(MD5.HashMD5(req.Senha))
                },
                UsuarioPerfil = new List<UsuarioPerfilDto>() { new UsuarioPerfilDto("Colaborador") }
            };

            _appService.CadastrarColaborador(dto, out int statusCode, out object dados);

            return StatusCode(statusCode, dados);

        }

        /*

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

        */

        #endregion

    }
}