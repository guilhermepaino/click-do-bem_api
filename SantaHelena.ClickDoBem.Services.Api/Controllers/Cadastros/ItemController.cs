﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros;
using SantaHelena.ClickDoBem.Services.Api.Model.Response.Cadastros;
using SantaHelena.ClickDoBem.Services.Api.Model.Response.Credenciais;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers.Cadastros
{

    /// <summary>
    /// API de Item
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/Item")]
    public class ItemController : CdbApiControllerBase
    {

        #region Objetos/Variáveis Locais

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        protected readonly IItemAppService _appService;
        protected readonly IHostingEnvironment _hostingEnvironment;
        protected readonly IAppUser _appUser;

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância da API
        /// </summary>
        public ItemController
        (
            IItemAppService appService,
            IHostingEnvironment hostingEnvironment,
            IAppUser appUser
        )
        {
            _appService = appService;
            _hostingEnvironment = hostingEnvironment;
            _appUser = appUser;
        }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Converte um objeto Dto em Response
        /// </summary>
        /// <param name="dto">Objeto Dto para conversão</param>
        protected ItemResponse ConverterDtoEmResponse(ItemDto dto)
        {

            ItemResponse resp = new ItemResponse()
            {
                Id = dto.Id,
                DataInclusao = dto.DataInclusao,
                DataAlteracao = dto.DataAlteracao,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                TipoItem = dto.TipoItem.Descricao,
                Anonimo = dto.Anonimo,
                Categoria = new CategoriaSimpleResponse()
                {
                    Id = dto.Categoria.Id,
                    Descricao = dto.Categoria.Descricao,
                    Pontuacao = dto.Categoria.Pontuacao,
                    GerenciadaRh = dto.Categoria.GerenciadaRh
                },
                Usuario = new UsuarioSimpleResponse()
                {
                    Id = dto.Usuario.Id,
                    Nome = dto.Usuario.Nome,
                    CpfCnpj = dto.Usuario.CpfCnpj
                }
            };

            return resp;

        }

        /// <summary>
        /// Converte uma lista de objetos Dto em lista de objetos Response
        /// </summary>
        /// <param name="dto">Objeto Dto para conversão</param>
        protected IEnumerable<ItemResponse> ConverterDtoEmResponse(IEnumerable<ItemDto> dto)
        {
            return dto.ToList().Select(x => ConverterDtoEmResponse(x));
        }

        #endregion

        #region Métodos/EndPoints Api

        /// <summary>
        /// Inserir um novo registro de item
        /// </summary>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição: ItemInsertRequest
        ///     {
        ///         "titulo": "Fralda Descartável",
        ///         "descricao": "Fralda para criança até 5 meses (tamanho RN, P e M)",
        ///         "tipoItem": "1",
        ///         "categoria": "Higiene e limpeza",
        ///         "anonimo": false
        ///      }
        ///      
        ///     Para o campo tipoItem informe: 1=Necessidade ou 2=Doação
        /// 
        ///     Resposta:
        ///     {
        ///         "sucesso": true,
        ///         "mensagem": {
        ///             "id": "fe9b1395-3753-4cb1-9b4e-23c246c04282"
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
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPost]
        public IActionResult Inserir([FromBody]ItemInsertRequest req)
        {

            if (!ModelState.IsValid)
                return Response<ItemInsertRequest>(req);

            ItemDto dto = new ItemDto()
            {
                Titulo = req.Titulo,
                Descricao = req.Descricao,
                TipoItem = new TipoItemDto() { Descricao = (req.TipoItem.Equals(1) ? "Necessidade" : "Doação") },
                Categoria = new CategoriaDto() { Descricao = req.Categoria },
                Usuario = new UsuarioDto() { Id = _appUser.Id },
                Anonimo = req.Anonimo
            };

            _appService.Inserir(dto, out int statusCode, out object dados);

            return StatusCode(statusCode, dados);

        }

        /// <summary>
        /// Inserir um novo registro de item
        /// </summary>
        /// <remarks>
        /// Contrato
        /// 
        ///     Requisição: ItemRequest
        ///     {
        ///         "titulo": "Fralda Descartável Infantil",
        ///         "descricao": "Fralda para criança até 5 meses (tamanho RN, P e M)",
        ///         "tipoItem": "Necessidade",
        ///         "categoria": "Higiene e limpeza",
        ///         "anonimo": false
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
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpPut]
        public IActionResult Atualizar([FromBody]ItemUpdateRequest req)
        {

            if (!ModelState.IsValid)
                return Response<ItemInsertRequest>(req);

            var dto = new ItemDto()
            {
                Id = req.Id,
                Titulo = req.Titulo,
                Descricao = req.Descricao,
                TipoItem = new TipoItemDto() { Descricao = (req.TipoItem.Equals(1) ? "Necessidade" : "Doação") },
                Categoria = new CategoriaDto() { Descricao = req.Categoria },
                Usuario = new UsuarioDto() { Id = _appUser.Id },
                Anonimo = req.Anonimo
            };

            _appService.Atualizar(dto, out int statusCode, out object dados);

            return StatusCode(statusCode, dados);

        }

        /// <summary>
        /// Excluir o registro
        /// </summary>
        /// <param name="id">Id do registro</param>
        /// <remarks>
        /// 
        ///     Requisição: DELETE
        ///     url: [URI]/api/versao/item/2ef307a6-c4a5-11e8-8776-0242ac110006
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
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpDelete("{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            _appService.Excluir(id, out int statusCode, out object dados);
            return StatusCode(statusCode, dados);
        }

        /// <summary>
        /// Listar todos os registros
        /// </summary>
        /// <remarks>
        /// Contrato
        ///
        ///     Requisição (parâmetro tipoItem = opcional)
        ///     <?tipoItem=N>
        ///     
        ///     onde N pode ser:
        ///     
        ///         1 = Necessidade
        ///         2 = Doação
        ///     
        ///     Resposta (array)
        ///     [
        ///         {
        ///             "id": "guid",
        ///             "dataInclusao": "YYYY-MM-DDThh:mm:ss",
        ///             "dataAlteracao": "YYYY-MM-DDThh:mm:ss",
        ///             "titulo": "string",
        ///             "descricao": "string",
        ///             "tipoItem": "string",
        ///             "categoria": {
        ///                 "id": "guid",
        ///                 "descricao": "string",
        ///                 "pontuacao": int,
        ///                 "gerenciadaRh": bool
        ///             },
        ///             "usuario": {
        ///                 "id": "guid",
        ///                 "nome": "string",
        ///                 "cpfCnpj": "string"
        ///             },
        ///             "anonimo": bool
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <returns>Lista dos registros cadastrados</returns>
        /// <response code="200">Retorna a lista de registros cadastrados</response>
        /// <response code="400">Requisição inválida, veja detalhes na mensagem</response>
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet]
        public IActionResult Listar([FromQuery]int? tipoItem)
        {
            IEnumerable<ItemDto> registros = null;
            if (tipoItem == null)
            {
                registros = _appService.ObterTodos();
            }
            else
            {
                switch (tipoItem.Value)
                {
                    case 1:
                        registros = _appService.ObterNecessidades();
                        break;
                    case 2:
                        registros = _appService.ObterDoacoes();
                        break;
                    default:
                        return BadRequest(new { sucesso = "false", mensagem = "O tipo de item deve ser 1=Necessidade ou 2=Doação" });
                }
            }

            return Ok(ConverterDtoEmResponse(registros));
        }

        /// <summary>
        /// Buscar registro de item pelo Id (guid)
        /// </summary>
        /// <remarks>
        /// Contrato
        ///
        ///     Requisição
        ///     url: [URI]/api/versao/item/2ef307a6-c4a5-11e8-8776-0242ac110006
        ///     
        ///     Resposta
        ///         {
        ///             "id": "guid",
        ///             "dataInclusao": "YYYY-MM-DDThh:mm:ss",
        ///             "dataAlteracao": "YYYY-MM-DDThh:mm:ss",
        ///             "titulo": "string",
        ///             "descricao": "string",
        ///             "tipoItem": "string",
        ///             "categoria": {
        ///                 "id": "guid",
        ///                 "descricao": "string",
        ///                 "pontuacao": int,
        ///                 "gerenciadaRh": bool
        ///             },
        ///             "usuario": {
        ///                 "id": "guid",
        ///                 "nome": "string",
        ///                 "cpfCnpj": "string"
        ///             },
        ///             "anonimo": bool
        ///         }
        ///     
        /// </remarks>
        /// <returns>Dados do registro localizado ou null se não encontrado</returns>
        /// <response code="200">Sucesso na operação de busca</response>
        /// <response code="401">Acesso-Negado (Token inválido ou expirado)</response>
        /// <response code="500">Se ocorrer alguma falha no processamento da request</response>
        [HttpGet("{id:guid}")]
        public IActionResult BuscarPorId(Guid id)
        {
            return Ok(_appService.ObterPorId(id));
        }

        #endregion

    }
}