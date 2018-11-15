using Microsoft.AspNetCore.Http;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaHelena.ClickDoBem.Application.Services.Cadastros
{
    public class CampanhaAppService : AppServiceBase<CampanhaDto, Campanha>, ICampanhaAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly ICampanhaDomainService _dmn;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public CampanhaAppService
        (
            IUnitOfWork uow,
            ICampanhaDomainService dmn
        )
        {
            _uow = uow;
            _dmn = dmn;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Converter entidade em Dto
        /// </summary>
        /// <param name="campanha">Objeto entidade</param>
        protected override CampanhaDto ConverterEntidadeEmDto(Campanha campanha)
        {
            return new CampanhaDto()
            {
                Id = campanha.Id,
                DataInclusao = campanha.DataInclusao,
                DataAlteracao = campanha.DataAlteracao,
                Descricao = campanha.Descricao,
                DataInicial = campanha.DataInicial,
                DataFinal = campanha.DataFinal,
                Prioridade = campanha.Prioridade
            };
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public IEnumerable<CampanhaDto> ObterTodos()
        {
            IEnumerable<Campanha> result = _dmn.ObterTodos().OrderBy(x => x.DataInclusao);
            if (result == null)
                return null;

            return
                (
                    from r in result
                    select ConverterEntidadeEmDto(r)

                ).ToList();
        }

        public IEnumerable<CampanhaDto> ObterAtivas()
        {
            IEnumerable<Campanha> result = _dmn.Obter(x => 
                x.DataInicial <= DateTime.Now &&
                x.DataFinal >= DateTime.Now
            ).ToList();

            if (result == null)
                return null;

            return
                (
                    from r in result
                    select ConverterEntidadeEmDto(r)

                ).ToList();
        }

        /// <summary>
        /// Obter registro pelo id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public CampanhaDto ObterPorId(Guid id)
        {
            Campanha result = _dmn.ObterPorId(id);
            if (result == null)
                return null;
            return ConverterEntidadeEmDto(result);
        }

        /// <summary>
        /// Inserir registro na base
        /// </summary>
        /// <param name="dto">Objeto de transporte de dados de campanha</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="mensagem">Objeto de saída de dados (mensagem)</param>
        public void Inserir(CampanhaDto dto, out int statusCode, out object mensagem)
        {

            StringBuilder criticas = new StringBuilder();

            // Prioridade
            if (dto.Prioridade < 0 || dto.Prioridade > 3)
                criticas.Append("A prioridade deve conter um dos seguintes valores: 0=baixa / 1=Normal / 2=Alta / 3=Altíssima|");

            if (dto.DataFinal < dto.DataInicial)
                criticas.Append("A data inicial não pode ser superior a data final|");

            if (criticas.Length > 0)
            {
                mensagem = criticas.ToString().Substring(0, (criticas.Length - 1)).Replace("|", "\r\n");
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {

                Campanha entidade = new Campanha()
                {
                    Descricao = dto.Descricao,
                    DataInicial = dto.DataInicial,
                    DataFinal = dto.DataFinal,
                    Prioridade = dto.Prioridade
                };

                if (!entidade.EstaValido())
                {
                    mensagem = entidade.ValidationResult.ToString();
                    statusCode = StatusCodes.Status400BadRequest;
                }
                else
                {

                    _dmn.Adicionar(entidade);
                    _uow.Efetivar();

                    mensagem = new { Id = entidade.Id.ToString() };
                    dto.Id = entidade.Id;
                    statusCode = StatusCodes.Status200OK;

                }

            }

        }

        /// <summary>
        /// Alterar um registro existente
        /// </summary>
        /// <param name="dto">Objeto de transporte de dados de campanha</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="mensagem">Variável string de saída de dados (mensagem)</param>
        public void Atualizar(CampanhaDto dto, out int statusCode, out string mensagem)
        {

            // Verificando Item
            Campanha entidade = _dmn.ObterPorId(dto.Id);
            if (entidade == null)
            {
                mensagem = "Item não encontrado";
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {

                StringBuilder criticas = new StringBuilder();

                // Prioridade
                if (dto.Prioridade < 0 || dto.Prioridade > 3)
                    criticas.Append("A prioridade deve conter um dos seguintes valores: 0=baixa / 1=Normal / 2=Alta / 3=Altíssima|");

                if (dto.DataFinal < dto.DataInicial)
                    criticas.Append("A data inicial não pode ser superior a data final|");

                if (criticas.Length > 0)
                {
                    mensagem = criticas.ToString().Substring(0, (criticas.Length - 1)).Replace("|", "\r\n");
                    statusCode = StatusCodes.Status400BadRequest;
                }
                else
                {

                    entidade.Descricao = dto.Descricao;
                    entidade.DataInicial = dto.DataInicial;
                    entidade.DataFinal = dto.DataFinal;
                    entidade.Prioridade = dto.Prioridade;

                    if (!entidade.EstaValido())
                    {
                        mensagem = entidade.ValidationResult.ToString();
                        statusCode = StatusCodes.Status400BadRequest;
                    }
                    else
                    {

                        _dmn.Atualizar(entidade);
                        _uow.Efetivar();

                        mensagem = "Registro alterado com sucesso";
                        statusCode = StatusCodes.Status200OK;

                    }

                }

            }

        }

        /// <summary>
        /// Excluir registro
        /// </summary>
        /// <param name="id">Id do registro a ser excluído</param>
        /// <param name="webRootPath">local da pasta wwwroot</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="dados">Objeto de saída de dados (mensagem)</param>
        public void Excluir(Guid id, string webRootPath, out int statusCode, out object dados)
        {

            Campanha entidade = _dmn.ObterPorId(id);
            if (entidade == null)
            {
                dados = new { sucesso = false, mensagem = "Registro de campanha não encontrado" };
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {

                _dmn.Excluir(id);
                _uow.Efetivar();

                ////TODO: Remover imagem
                //Task.Factory.StartNew(() =>
                //{
                //    Thread.Sleep(2000);
                //    RemoverArquivosItemExcluido(pastaWwwRoot, arquivosRemover);
                //});

                dados = new { sucesso = true, mensagem = "Campanha excluída com sucesso" };
                statusCode = StatusCodes.Status200OK;
            }


        }

        /// <summary>
        /// Encerrar uma campnha imediatamente
        /// </summary>
        /// <param name="id">Id da campanha</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="dados">Objeto de saída de dados (mensagem)</param>
        public void EncerrarCampanha(Guid id, out int statusCode, out object dados)
        {
            if (_dmn.ObterPorId(id) == null)
            {
                statusCode = StatusCodes.Status400BadRequest;
                dados = new { Sucesso = false, Mensagem = "Campanha não encontrada" };
            }
            else
            {
                _dmn.EncerrarCampanha(id);
                statusCode = StatusCodes.Status200OK;
                dados = new { Sucesso = false, Mensagem = "Campanha encerrada com sucesso" };
            }
        }

        #endregion

    }
}
