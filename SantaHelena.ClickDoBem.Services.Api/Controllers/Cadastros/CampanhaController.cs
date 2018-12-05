using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros;
using System;
using System.IO;
using System.Linq;

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
        protected readonly string _caminho;

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
            _caminho = Directory.GetDirectories(_hostingEnvironment.WebRootPath).Where(x => x.EndsWith("images")).SingleOrDefault();
        }

        #endregion

        #region Métodos/EndPoints Api


        // ----- GET --------------------------------------------------------------
        // ------------------------------------------------------------------------

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
        ///             "dataFinal": "0001-01-01T00:00:00",
        ///             "imagem": "string"
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
        ///         "dataFinal": "0001-01-01T00:00:00",
        ///         "imagem": "string"
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

        // ----- POST -------------------------------------------------------------
        // ------------------------------------------------------------------------

        /// <summary>
        /// Inserir um novo registro de campanha
        /// </summary>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição: ItemInsertRequest
        ///     {
        ///         "descricao": "string",
        ///         "dataInicial": "YYYY-MM-DDTHH:MM:SS",
        ///         "dataFinal": "YYYY-MM-DDTHH:MM:SS",
        ///         "prioridade": int
        ///      }
        ///      
        ///     Para o campo prioridade informe: 0=baixa / 1=Normal / 2=Alta / 3=Altíssima
        /// 
        ///     Resposta:
        ///     {
        ///         "sucesso": true,
        ///         "mensagem": {
        ///             "id": "289fbf37-eb30-4a18-ab28-ee394aa10e87",
        ///             "imagem": "string"
        ///         }
        ///     }
        ///     
        ///     Validações apresentadas em array, exemplo:
        ///     {
        ///         "sucesso": false,
        ///         "Mensagem": [ 
        ///             "Critica1, 
        ///             "Critica2"
        ///         ]
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Sucesso na gravação - retornará o Id do registro no campo mensagem</response>
        /// <response code="400">Requisição inválida, detalhes informado no campo mensagem</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPost]
        public IActionResult Inserir([FromBody]CampanhaInsertRequest req)
        {

            if (!ModelState.IsValid)
                return Response<CampanhaInsertRequest>(req);

            CampanhaDto dto = new CampanhaDto()
            {
                Descricao = req.Descricao,
                DataInicial = req.DataInicial,
                DataFinal = req.DataFinal,
                Prioridade = req.Prioridade
            };

            _appService.Inserir(dto, out int statusCode, out object mensagem);

            object dados = dados = new { Sucesso = statusCode.Equals(StatusCodes.Status200OK), Mensagem = mensagem };

            return StatusCode(statusCode, dados);

        }

        /// <summary>
        /// Carregar uma imagem para a campanha (sobrescreve se existir)
        /// </summary>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição: ItemInsertRequest
        ///     {
        ///         "campanhaId": "guid",
        ///         "imagemBase64": "string_base64"
        ///      }
        /// 
        ///     Resposta:
        ///     {
        ///         "sucesso": true,
        ///         "mensagem": "imagem carregada com sucesso",
        ///         "imagem": "caminho/imagem.jpg"
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Sucesso na gravação</response>
        /// <response code="400">Requisição inválida, detalhes informado no campo mensagem</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPost("imagem")]
        public IActionResult CarregarImagem([FromBody]CampanhaImagemRequest req)
        {
            _appService.CarregarImagem(req.CampanhaId, req.ImagemBase64, Path.Combine(_caminho, "campanha"), out int statusCode, out object dadosRetorno);
            return StatusCode(statusCode, dadosRetorno);
        }

        // ----- PUT --------------------------------------------------------------
        // ------------------------------------------------------------------------

        /// <summary>
        /// Alterar uma campanha existente
        /// </summary>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição: ItemRequest
        ///     {
        ///         "id": "guid",
        ///         "descricao": "string",
        ///         "dataInicial": "YYYY-MM-DDTHH:MM:SS",
        ///         "dataFinal": "YYYY-MM-DDTHH:MM:SS",
        ///         "prioridade": int
        ///      }
        ///      
        ///     Resposta:
        ///     {
        ///         "sucesso": true,
        ///         "mensagem": "Registro alterado com sucesso"
        ///     }
        /// 
        ///     Validações apresentadas em array, exemplo:
        ///     {
        ///         "sucesso": false,
        ///         "mensagem": [
        ///             "Critica1,
        ///             "Critica2"
        ///         ]
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Sucesso na gravação</response>
        /// <response code="400">Requisição inválida, detalhes informado no campo mensagem</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPut]
        public IActionResult Atualizar([FromBody]CampanhaUpdateRequest req)
        {

            if (!ModelState.IsValid)
                return Response<CampanhaUpdateRequest>(req);

            CampanhaDto dto = new CampanhaDto()
            {
                Id = req.Id,
                Descricao = req.Descricao,
                DataInicial = req.DataInicial,
                DataFinal = req.DataFinal,
                Prioridade = req.Prioridade
            };

            _appService.Atualizar(dto, out int statusCode, out string mensagem);

            return StatusCode(statusCode, new { sucesso = statusCode.Equals(StatusCodes.Status200OK), mensagem });

        }

        // ----- DELETE -----------------------------------------------------------
        // ------------------------------------------------------------------------

        /// <summary>
        /// Excluir o registro
        /// </summary>
        /// <param name="id">Id do registro</param>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição: DELETE
        ///     url: [URI]/api/versao/campanha/2ef307a6-c4a5-11e8-8776-0242ac110006
        /// 
        ///     Resposta:
        ///     {
        ///         "sucesso": true,
        ///         "mensagem": "Item excluído com sucesso"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Sucesso na exclusão</response>
        /// <response code="400">Requisição inválida, detalhes informado no campo mensagem</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpDelete("{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            _appService.Excluir(id, _hostingEnvironment.WebRootPath, out int statusCode, out object dados);
            return StatusCode(statusCode, dados);
        }

        /// <summary>
        /// Encerrar a campanha
        /// </summary>
        /// <param name="id">Id da campanha</param>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição: POST
        ///     url: [URI]/api/versao/campanha/encerrar/2ef307a6-c4a5-11e8-8776-0242ac110006
        /// 
        ///     Resposta:
        ///     {
        ///         "sucesso": true,
        ///         "mensagem": "Campanha encerrada com sucesso"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Sucesso no encerramento da campanha</response>
        /// <response code="400">Requisição inválida, detalhes informado no campo mensagem</response>
        /// <response code="403">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPost("encerrar/{id:guid}")]
        public IActionResult EncerrarCampanha(Guid id)
        {
            _appService.EncerrarCampanha(id, out int statusCode, out object dados);
            return StatusCode(statusCode, dados);
        }

        #endregion

    }
}