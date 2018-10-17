using Microsoft.AspNetCore.Http;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Application.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Core.ReportDto;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SantaHelena.ClickDoBem.Application.Services.Cadastros
{
    public class ItemAppService : AppServiceBase<ItemDto, Item>, IItemAppService
    {

        #region Objetos/Variáveis Locais

        protected enum TipoBuscaRegistros
        {
            Todos,
            Necessidade,
            Doacao
        }

        protected readonly IUnitOfWork _uow;
        protected readonly IItemDomainService _dmn;
        protected readonly ICategoriaDomainService _categoriaDomain;
        protected readonly ITipoItemDomainService _tipoItemDomain;
        protected readonly IItemImagemDomainService _imagemDomain;
        protected readonly IUsuarioDomainService _usuarioDomain;
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
            IItemImagemDomainService imagemDomain,
            IUsuarioDomainService usuarioDomain,
            IAppUser usuario
        )
        {
            _uow = uow;
            _dmn = dmn;
            _categoriaDomain = categoriaDomain;
            _tipoItemDomain = tipoItemDomain;
            _imagemDomain = imagemDomain;
            _usuarioDomain = usuarioDomain;
            _usuario = usuario;
        }

        #endregion

        #region Métodos Locais

        protected void CarregaRelacoes(Item item)
        {
            List<Item> lista = new List<Item>();
            lista.Add(item);
            CarregaRelacoes(lista);
            item = lista.FirstOrDefault();
        }

        protected void CarregaRelacoes(IEnumerable<Item> itens)
        {

            IEnumerable<Categoria> categorias = _categoriaDomain.ObterTodos();
            IEnumerable<TipoItem> tiposItem = _tipoItemDomain.ObterTodos();
            IEnumerable<Usuario> usuarios = _usuarioDomain.ObterPorLista(itens.Select(x => x.UsuarioId.Value).Distinct().ToList());
            IEnumerable<ItemImagem> imagens = _imagemDomain.ObterPorLista(itens.Select(x => x.Id).Distinct().ToList());


            itens
                .ToList()
                .ForEach(i =>
                {
                    i.Categoria = categorias.Where(f => f.Id.Equals(i.CategoriaId)).FirstOrDefault();
                    i.TipoItem = tiposItem.Where(f => f.Id.Equals(i.TipoItemId)).FirstOrDefault();
                    i.Usuario = usuarios.Where(f => f.Id.Equals(i.UsuarioId)).FirstOrDefault();
                    i.Imagens = imagens.Where(f => f.ItemId.Equals(i.Id)).ToList();
                });

        }

        /// <summary>
        /// Remover os itens ocultos (Gerenciada RH e Anônimo)
        /// </summary>
        /// <param name="itensDto">Item Dto</param>
        protected ItemDto RemoverOcultos(ItemDto itemDto)
        {
            IList<ItemDto> lista = new List<ItemDto>();
            lista.Add(itemDto);
            lista = RemoverOcultos(lista);
            return lista.FirstOrDefault();
        }

        /// <summary>
        /// Remover os itens ocultos (Gerenciada RH e Anônimo)
        /// </summary>
        /// <param name="itensDto">Lista de Dto de Item</param>
        protected IList<ItemDto> RemoverOcultos(IList<ItemDto> itensDto)
        {

            Guid usuarioId = _usuario.Id;

            IList<ItemDto> result = new List<ItemDto>();
            foreach (ItemDto item in itensDto)
            {
                if ((!(item.Categoria.GerenciadaRh || item.Anonimo) || item.Usuario.Id.Equals(usuarioId)) || _usuario.Perfis.Contains("Admin"))
                    result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Remover fisicamente o arquivo
        /// </summary>
        /// <param name="imagens"></param>
        protected void RemoverArquivosItemExcluido(string caminho, IEnumerable<string> imagens)
        {

            foreach (string img in imagens)
            {
                try
                {
                    string arq = img.Replace("/", @"\");
                    string nomeCompleto = Path.Combine(caminho, arq);
                    File.Delete(nomeCompleto);
                }
                finally { /* Nada a fazer, segue a vida */ }
            }
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
                    UsuarioDados = (item.Usuario.UsuarioDados != null ? new UsuarioDadosDto()
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
                    } : null),
                    UsuarioPerfil = item.Usuario.Perfis.Select(x => x.Perfil).ToList()
                },
                Imagens = item.Imagens.Select(i => new ItemImagemDto()
                {
                    Id = i.Id,
                    DataInclusao = i.DataInclusao,
                    DataAlteracao = i.DataAlteracao,
                    ItemId = i.ItemId.Value,
                    NomeOriginal = i.NomeOriginal,
                    Caminho = i.Caminho
                })

            };
        }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Obter registros com base nos filtros
        /// </summary>
        /// <param name="tipo">Tipo de busca de registro</param>
        protected IEnumerable<ItemDto> ObterRegistros(TipoBuscaRegistros tipo)
        {

            IEnumerable<Item> result = null;

            switch (tipo)
            {
                case TipoBuscaRegistros.Necessidade:
                    result = _dmn.ObterNecessidades();
                    break;
                case TipoBuscaRegistros.Doacao:
                    result = _dmn.ObterDoacoes();
                    break;
                default:
                    result = _dmn.ObterTodos();
                    break;
            }

            if (result == null)
                return null;

            CarregaRelacoes(result);
            IList<ItemDto> resultDto =
            (
                from r in result
                select ConverterEntidadeEmDto(r)

            ).ToList();

            resultDto = RemoverOcultos(resultDto);

            return resultDto;


        }

        /// <summary>
        /// Valida dos dados de carregamento de Imagem
        /// </summary>
        /// <param name="nomeImagem">Nome da imagem</param>
        /// <param name="imagemBase64">Expressão base64 da imagem</param>
        /// <param name="itemId">Id do item</param>
        /// <param name="item">Objeto de saída do item</param>
        /// <returns></returns>
        protected string ValidaDadosCarregamentoImagem(string nomeImagem, string imagemBase64, Guid itemId, out Item item)
        {

            StringBuilder criticas = new StringBuilder();

            if (string.IsNullOrWhiteSpace(nomeImagem))
                criticas.Append("Nome da imagem não informado|");

            if (string.IsNullOrWhiteSpace(imagemBase64))
                criticas.Append("Expressao Base64 da imagem não informada|");
            else
            {
                imagemBase64 = imagemBase64.Trim();
                if (!((imagemBase64.Length % 4 == 0) && Regex.IsMatch(imagemBase64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None)))
                    criticas.Append("Base64 inválida|");
            }

            item = _dmn.ObterPorId(itemId);
            if (item == null)
                criticas.Append($"Item \"{itemId}\" não necontrado|");

            return criticas.ToString();

        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obter registro pelo id
        /// </summary>
        /// <param name="id">Id do registro</param>
        public ItemDto ObterPorId(Guid id)
        {
            Item item = _dmn.ObterPorId(id);
            if (item == null)
                return null;

            CarregaRelacoes(item);
            ItemDto resultDto = ConverterEntidadeEmDto(item);
            resultDto = RemoverOcultos(resultDto);

            return resultDto;

        }

        /// <summary>
        /// Obter registros todos os registros
        /// </summary>
        public IEnumerable<ItemDto> ObterTodos()
        {
            return ObterRegistros(TipoBuscaRegistros.Todos);
        }

        /// <summary>
        /// Listar os registros de doações
        /// </summary>
        public IEnumerable<ItemDto> ObterDoacoes()
        {
            return ObterRegistros(TipoBuscaRegistros.Doacao);
        }

        /// <summary>
        /// Listar os registros de necessidade
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ItemDto> ObterNecessidades()
        {
            return ObterRegistros(TipoBuscaRegistros.Necessidade);
        }

        /// <summary>
        /// Inserir registro na base
        /// </summary>
        /// <param name="dto">Objeto de transporte de dados de item</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="mensagem">Objeto de saída de dados (mensagem)</param>
        public void Inserir(ItemDto dto, out int statusCode, out string mensagem)
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
                mensagem = criticas.ToString().Substring(0, (criticas.Length - 1)).Replace("|", "\r\n");
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
                    mensagem = entidade.ValidationResult.ToString();
                    statusCode = StatusCodes.Status400BadRequest;
                }
                else
                {

                    _dmn.Adicionar(entidade);
                    _uow.Efetivar();

                    mensagem = entidade.Id.ToString();
                    dto.Id = entidade.Id;
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
        /// <param name="pastaWwwRoot">local da pasta wwwroot</param>
        /// <param name="statusCode">Variável de saída de StatusCode</param>
        /// <param name="dados">Objeto de saída de dados (mensagem)</param>
        public void Excluir(Guid id, string pastaWwwRoot, out int statusCode, out object dados)
        {

            Item entidade = _dmn.ObterPorId(id);
            if (entidade == null)
            {
                dados = new { sucesso = false, mensagem = "Item não encontrado" };
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {

                CarregaRelacoes(entidade);
                IList<string> arquivosRemover = new List<string>();

                entidade.Imagens
                    .ToList()
                    .ForEach(i =>
                    {
                        arquivosRemover.Add(i.Caminho.Substring(1));
                        _imagemDomain.Excluir(i.Id);
                    });

                _dmn.Excluir(id);
                _uow.Efetivar();

                Task.Factory.StartNew(() => {
                    Thread.Sleep(2000);
                    RemoverArquivosItemExcluido(pastaWwwRoot, arquivosRemover);
                });

                dados = new { sucesso = true, mensagem = "Item éxcluído com sucesso" };
                statusCode = StatusCodes.Status200OK;
            }


        }

        /// <summary>
        /// Executar a pesquisa de itens com base nos critérios
        /// </summary>
        /// <param name="dataInicial">Data inicial do período</param>
        /// <param name="dataFinal">Data final do período</param>
        /// <param name="tipoItemId">Id do tipo de item</param>
        /// <param name="categoriaId">Id da categoria</param>
        public IEnumerable<ItemListaReportDto> Pesquisar(DateTime? dataInicial, DateTime? dataFinal, Guid? tipoItemId, Guid? categoriaId)
        {
            return _dmn.Pesquisar(dataInicial, dataFinal, tipoItemId, categoriaId);
        }

        /// <summary>
        /// Carregar uma imagem para um item
        /// </summary>
        /// <param name="itemId">Id do item</param>
        /// <param name="nomeImagem">Nome (título) da imagem</param>
        /// <param name="imagemBase64">Hash Base 64 da imagem</param>
        /// <param name="caminho">Caminho onde o arquivo será gravado</param>
        /// <param name="statusCode">Variável de saída do StatusCode</param>
        /// <param name="dadosRetorno">Variável de retorno da response</param>
        public void CarregarImagem(Guid itemId, string nomeImagem, string imagemBase64, string caminho, out int statusCode, out object dadosRetorno)
        {

            string criticas = ValidaDadosCarregamentoImagem(nomeImagem, imagemBase64, itemId, out Item item);

            if (!string.IsNullOrWhiteSpace(criticas))
            {
                statusCode = StatusCodes.Status400BadRequest;
                dadosRetorno = new
                {
                    sucesso = false,
                    mensagem = criticas.Substring(0, (criticas.Length - 1))
                };
            }
            else
            {
                bool sucesso = _dmn.CarregarImagem(item, nomeImagem, imagemBase64, caminho, out dadosRetorno);
                statusCode = (sucesso ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest);

            }


        }
        /// <summary>
        /// Remover uma imagem de um item
        /// </summary>
        /// <param name="id">Id da imagem</param>
        /// <param name="caminho">Caminho base das imagens</param>
        /// <param name="statusCode">Variável de saída do StatusCode</param>
        /// <param name="dadosRetorno">Variável de retorno da response</param>
        public void RemoverImagem(Guid id, string caminho, out int statusCode, out object dadosRetorno)
        {
            bool sucesso = _dmn.RemoverImagem(id, caminho, out dadosRetorno);
            statusCode = (sucesso ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest);
        }

        #endregion

    }
}
