using Microsoft.AspNetCore.Http;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
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
            IAppUser usuario
        )
        {
            _uow = uow;
            _dmn = dmn;
            _categoriaDomain = categoriaDomain;
            _tipoItemDomain = tipoItemDomain;
            _usuario = usuario;
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
        /// <param name="dto"></param>
        /// <param name="statusCode"></param>
        /// <param name="dados"></param>
        public bool Inserir(ItemDto dto, out int statusCode, out object dados)
        {

            StringBuilder criticas = new StringBuilder();
            statusCode = StatusCodes.Status200OK;

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
                return false;
            }

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
                return false;
            }

            _dmn.Adicionar(entidade);
            _uow.Efetivar();

            dados = new { sucesso = true, mensagem = new { Id = entidade.Id } };
            statusCode = StatusCodes.Status200OK;
            return true;


        }

        /// <summary>
        /// Inserir registro na base
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="statusCode"></param>
        /// <param name="dados"></param>
        public bool Atualizar(ItemDto dto, out int statusCode, out object dados)
        {


            StringBuilder criticas = new StringBuilder();
            statusCode = StatusCodes.Status200OK;

            // Verificando Item
            Item entidade = _dmn.ObterPorId(dto.Id);
            if (entidade == null)
            {
                dados = new { sucesso = false, mensagem = "Item não encontrado" };
                statusCode = StatusCodes.Status400BadRequest;
                return false;
            }

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
                return false;
            }

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
                return false;
            }

            _dmn.Atualizar(entidade);
            _uow.Efetivar();

            dados = new { sucesso = true, mensagem = "Registro alterado com sucesso" };
            statusCode = StatusCodes.Status200OK;
            return true;

        }

        #endregion

    }
}
