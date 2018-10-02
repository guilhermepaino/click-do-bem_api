using Microsoft.AspNetCore.Http;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaHelena.ClickDoBem.Application.Services.Cadastros
{
    public class ItemAppService : AppServiceBase<ItemDto, Item>, IItemAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly IItemDomainService _dmn;
        protected readonly ICategoriaDomainService _categoriaDomain;
        protected readonly ITipoItemDomainService _tipoItemDomain;
        protected readonly IItemImagemDomainService _imagemDomain;
        protected readonly IAppUser _usuario;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public ItemAppService
        (
            IUnitOfWork uow,
            IItemDomainService dmn,
            ICategoriaDomainService categoriaDomain,
            ITipoItemDomainService tipoItemDomain,
            IAppUser usuario,
            IItemImagemDomainService imagemDomain
        )
        {
            _uow = uow;
            _dmn = dmn;
            _categoriaDomain = categoriaDomain;
            _tipoItemDomain = tipoItemDomain;
            _usuario = usuario;
            _imagemDomain = imagemDomain;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Converter entidade em Dto
        /// </summary>
        /// <param name="item">Objeto entidade</param>
        protected override ItemDto ConverterEntidadeEmDto(Item item)
        {
            return new ItemDto()
            {
                Id = item.Id,
                DataInclusao = item.DataInclusao,
                DataAlteracao = item.DataAlteracao,
                Titulo = item.Titulo,
                Descricao = item.Descricao,
                Anonimo = item.Anonimo,
                Categoria = new CategoriaDto()
                {
                    Id = item.Categoria.Id,
                    DataInclusao = item.Categoria.DataInclusao,
                    DataAlteracao = item.Categoria.DataAlteracao,
                    Descricao = item.Categoria.Descricao,
                    Pontuacao = item.Categoria.Pontuacao,
                    GerenciadaRh = item.Categoria.GerenciadaRh
                },
                TipoItem = new TipoItemDto()
                {
                    Id = item.TipoItem.Id,
                    DataInclusao = item.TipoItem.DataInclusao,
                    DataAlteracao = item.TipoItem.DataAlteracao,
                    Descricao = item.TipoItem.Descricao
                },
                Usuario = new UsuarioDto()
                {
                    Id = item.Usuario.Id,
                    DataInclusao = item.Usuario.DataInclusao,
                    DataAlteracao = item.Usuario.DataAlteracao,
                    CpfCnpj = item.Usuario.CpfCnpj,
                    Nome = item.Usuario.Nome,
                    UsuarioLogin = new UsuarioLoginDto()
                    {
                        Login = item.Usuario.UsuarioLogin.Login,
                        Senha = "*ENCRYPTED*"
                    },
                    UsuarioDados = new UsuarioDadosDto()
                    {
                        Id = item.Usuario.UsuarioDados.Id,
                        DataInclusao = item.Usuario.UsuarioDados.DataInclusao,
                        DataAlteracao = item.Usuario.UsuarioDados.DataAlteracao,
                        DataNascimento = item.Usuario.UsuarioDados.DataNascimento,
                        Logradouro = item.Usuario.UsuarioDados.Logradouro,
                        Numero = item.Usuario.UsuarioDados.Numero,
                        Complemento = item.Usuario.UsuarioDados.Complemento,
                        Bairro = item.Usuario.UsuarioDados.Bairro,
                        Cidade = item.Usuario.UsuarioDados.Cidade,
                        UF = item.Usuario.UsuarioDados.UF,
                        CEP = item.Usuario.UsuarioDados.CEP,
                        TelefoneCelular = item.Usuario.UsuarioDados.TelefoneCelular,
                        TelefoneFixo = item.Usuario.UsuarioDados.TelefoneFixo,
                        Email = item.Usuario.UsuarioDados.Email
                    }

                }

            };
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registros pelo perfil
        /// </summary>
        public IEnumerable<ItemDto> ObterTodos()
        {
            IEnumerable<Item> result = _dmn.ObterTodos();
            if (result == null)
                return null;

            return
                (
                    from r in result
                    select ConverterEntidadeEmDto(r)

                ).ToList();
        }

        /// <summary>
        /// Inserir registro na base
        /// </summary>
        /// <param name="dto">Objeto de transporte de dados de item</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="dados">Objeto de saída de dados (mensagem)</param>
        public void Inserir(ItemDto dto, out int statusCode, out object dados)
        {

            StringBuilder criticas = new StringBuilder();

            // Verificando TipoItem
            TipoItem tipoItem = _tipoItemDomain.ObterPorDescricao(dto.TipoItem.Descricao);
            if (tipoItem == null)
                criticas.Append("Tipo de Item inválido|");


            // Verificando Categoria
            Categoria categoria = _categoriaDomain.ObterPorDescricao(dto.Categoria.Descricao);
            if (categoria == null)
                criticas.Append("Categoria inválida|");

            if (criticas.Length > 0)
            {
                dados = new { sucesso = false, mensagem = criticas.ToString().Substring(0, (criticas.Length - 1)).Replace("|", "\r\n") };
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {

                Item entidade = new Item()
                {
                    Titulo = dto.Titulo,
                    Descricao = dto.Descricao,
                    TipoItemId = tipoItem.Id,
                    CategoriaId = categoria.Id,
                    UsuarioId = _usuario.Id,
                    Anonimo = dto.Anonimo
                };

                if (!entidade.EstaValido())
                {
                    dados = new { sucesso = false, mensagem = entidade.ValidationResult.ToString() };
                    statusCode = StatusCodes.Status400BadRequest;
                }
                else
                {

                    _dmn.Adicionar(entidade);
                    _uow.Efetivar();

                    dados = new { sucesso = true, mensagem = new { Id = entidade.Id } };
                    statusCode = StatusCodes.Status200OK;

                }

            }

        }

        /// <summary>
        /// Inserir registro na base
        /// </summary>
        /// <param name="dto">Objeto de transporte de dados de item</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="dados">Objeto de saída de dados (mensagem)</param>
        public void Atualizar(ItemDto dto, out int statusCode, out object dados)
        {


            StringBuilder criticas = new StringBuilder();

            // Verificando Item
            Item entidade = _dmn.ObterPorId(dto.Id);
            if (entidade == null)
            {
                dados = new { sucesso = false, mensagem = "Item não encontrado" };
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {

                // Verificando TipoItem
                TipoItem tipoItem = _tipoItemDomain.ObterPorDescricao(dto.TipoItem.Descricao);
                if (tipoItem == null)
                    criticas.Append("Tipo de Item inválido|");


                // Verificando Categoria
                Categoria categoria = _categoriaDomain.ObterPorDescricao(dto.Categoria.Descricao);
                if (categoria == null)
                    criticas.Append("Categoria inválida|");

                if (criticas.Length > 0)
                {
                    dados = new { sucesso = false, mensagem = criticas.ToString().Substring(0, (criticas.Length - 1)).Replace("|", "\r\n") };
                    statusCode = StatusCodes.Status400BadRequest;
                }
                else
                {

                    entidade.Titulo = dto.Titulo;
                    entidade.Descricao = dto.Descricao;
                    entidade.TipoItemId = tipoItem.Id;
                    entidade.CategoriaId = categoria.Id;
                    entidade.UsuarioId = _usuario.Id;
                    entidade.Anonimo = dto.Anonimo;

                    if (!entidade.EstaValido())
                    {
                        dados = new { sucesso = false, mensagem = entidade.ValidationResult.ToString() };
                        statusCode = StatusCodes.Status400BadRequest;
                    }
                    else
                    {

                        _dmn.Atualizar(entidade);
                        _uow.Efetivar();

                        dados = new { sucesso = true, mensagem = "Registro alterado com sucesso" };
                        statusCode = StatusCodes.Status200OK;

                    }

                }

            }

        }

        /// <summary>
        /// Excluir registro
        /// </summary>
        /// <param name="id">Id do registro a ser excluído</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="dados">Objeto de saída de dados (mensagem)</param>
        public void Excluir(Guid id, out int statusCode, out object dados)
        {

            // Verificando TipoItem
            // Verificando Item
            Item entidade = _dmn.ObterPorId(id);
            if (entidade == null)
            {
                dados = new { sucesso = false, mensagem = "Item não encontrado" };
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {

                IList<string> arquivosRemover = new List<string>();

                entidade.Imagens
                    .ToList()
                    .ForEach(i =>
                    {
                        arquivosRemover.Add(i.Caminho);
                        _imagemDomain.Excluir(i.Id);
                    });

                _dmn.Excluir(id);
                _uow.Efetivar();
                //TODO: Remover arquivo físico

                dados = new { sucesso = true, mensagem = "Item éxcluído com sucesso" };
                statusCode = StatusCodes.Status200OK;
            }


        }

        #endregion

    }
}
