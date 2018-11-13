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
        protected readonly IItemMatchDomainService _matchDomain;
        protected readonly IUsuarioDomainService _usuarioDomain;
        protected readonly ITipoMatchDomainService _tipoMatchDomain;
        protected readonly IValorFaixaDomainService _faixaDomain;
        protected readonly ICampanhaDomainService _campanhaDomain;
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
            IItemMatchDomainService matchDomain,
            IUsuarioDomainService usuarioDomain,
            ITipoMatchDomainService tipoMatchDomain,
            IValorFaixaDomainService faixaDomain,
            ICampanhaDomainService campanhaDomain,
            IAppUser usuario
        )
        {
            _uow = uow;
            _dmn = dmn;
            _categoriaDomain = categoriaDomain;
            _tipoItemDomain = tipoItemDomain;
            _imagemDomain = imagemDomain;
            _matchDomain = matchDomain;
            _usuarioDomain = usuarioDomain;
            _tipoMatchDomain = tipoMatchDomain;
            _faixaDomain = faixaDomain;
            _campanhaDomain = campanhaDomain;
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
            IEnumerable<ValorFaixa> faixas = _faixaDomain.ObterTodos();
            IEnumerable<Campanha> campanhas = _campanhaDomain.ObterTodos();

            itens
                .ToList()
                .ForEach(i =>
                {
                    i.Categoria = categorias.Where(f => f.Id.Equals(i.CategoriaId)).FirstOrDefault();
                    i.TipoItem = tiposItem.Where(f => f.Id.Equals(i.TipoItemId)).FirstOrDefault();
                    i.Usuario = usuarios.Where(f => f.Id.Equals(i.UsuarioId)).FirstOrDefault();
                    i.Imagens = imagens.Where(f => f.ItemId.Equals(i.Id)).ToList();
                    if (i.ValorFaixaId != null)
                        i.ValorFaixa = faixas.Where(f => f.Id.Equals(i.ValorFaixaId.Value)).FirstOrDefault();
                    if (i.CampanhaId != null)
                        i.Campanha = campanhas.Where(f => f.Id.Equals(i.CampanhaId)).FirstOrDefault();
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

                bool incluir = true;
                if (item.TipoItem.Descricao.ToLower().Equals("doação"))
                    if (item.Categoria.GerenciadaRh && !_usuario.Perfis.Contains("Admin") && !item.Usuario.Id.Equals(usuarioId))
                        incluir = false;

                if (incluir)
                {

                    if (item.Anonimo && !_usuario.Perfis.Contains("Admin") && !item.Usuario.Id.Equals(usuarioId))
                    {
                        item.Usuario.Nome = "** ANONIMO **";
                        item.Usuario.UsuarioDados.Logradouro = "-";
                        item.Usuario.UsuarioDados.Numero = "-";
                        item.Usuario.UsuarioDados.Complemento = "-";
                        item.Usuario.UsuarioDados.Bairro = "-";
                        item.Usuario.UsuarioDados.Cidade = "-";
                        item.Usuario.UsuarioDados.CEP = "00000-000";
                        item.Usuario.UsuarioDados.TelefoneCelular = "-";
                        item.Usuario.UsuarioDados.TelefoneFixo = "-";
                        item.Usuario.UsuarioDados.UF = "-";
                        item.Usuario.CpfCnpj = "-";
                    }
                    result.Add(item);

                }

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
                ValorFaixa = (item.ValorFaixa != null ? new ValorFaixaDto()
                {
                    Id = item.ValorFaixa.Id,
                    Descricao = item.ValorFaixa.Descricao,
                    ValorInicial = item.ValorFaixa.ValorInicial,
                    ValorFinal = item.ValorFaixa.ValorFinal,
                    Inativo = item.ValorFaixa.Inativo
                } : null),
                Campanha = (item.Campanha != null ? new CampanhaDto()
                {
                    Id = item.Campanha.Id,
                    Descricao = item.Campanha.Descricao,
                    DataInclusao = item.Campanha.DataInclusao,
                    DataAlteracao = item.Campanha.DataAlteracao,
                    DataInicial = item.Campanha.DataInicial,
                    DataFinal = item.Campanha.DataFinal
                } : null),
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
        /// Converter uma lista de entidade em uma lista de dto
        /// </summary>
        /// <param name="itens">Lista de entidades</param>
        protected IEnumerable<ItemDto> ConverterEntidadeEmDto(IEnumerable<Item> itens)
        {

            if (itens == null)
                return null;

            IList<ItemDto> result = new List<ItemDto>();
            foreach (Item item in itens)
                result.Add(ConverterEntidadeEmDto(item));

            return result;


        }

        /// <summary>
        /// Obter registros com base nos filtros
        /// </summary>
        /// <param name="tipo">Tipo de busca de registro</param>
        /// <param name="incluirItensMatches">Flag indicando se irá incluir itens de matches</param>
        protected IEnumerable<ItemDto> ObterRegistros(TipoBuscaRegistros tipo, bool incluirItensMatches)
        {

            IEnumerable<Item> result = null;

            switch (tipo)
            {
                case TipoBuscaRegistros.Necessidade:
                    result = _dmn.ObterNecessidades(incluirItensMatches);
                    break;
                case TipoBuscaRegistros.Doacao:
                    result = _dmn.ObterDoacoes(incluirItensMatches);
                    break;
                default:
                    result = _dmn.ObterTodos(incluirItensMatches);
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
            //else
            //{
            //    imagemBase64 = imagemBase64.Trim();
            //    if (!((imagemBase64.Length % 4 == 0) && Regex.IsMatch(imagemBase64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None)))
            //        criticas.Append("Base64 inválida|");
            //}

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
        /// Listar os registros de doações
        /// </summary>
        public IEnumerable<ItemDto> ObterDoacoes()
        {
            return ObterRegistros(TipoBuscaRegistros.Doacao, false);
        }

        /// <summary>
        /// Listar os registros de necessidade
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ItemDto> ObterNecessidades()
        {
            return ObterRegistros(TipoBuscaRegistros.Necessidade, false);
        }

        public IEnumerable<ItemDto> ObterTodos(bool incluirItensMatches)
        {
            return ObterRegistros(TipoBuscaRegistros.Todos, incluirItensMatches);
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
            Categoria categoria = _categoriaDomain.ObterPorId(dto.Categoria.Id);
            if (categoria == null)
                criticas.Append("Categoria inválida|");

            // Verificando faixa de valor
            if (dto.ValorFaixa != null)
            {
                ValorFaixa faixa = _faixaDomain.ObterPorId(dto.ValorFaixa.Id);
                if (faixa == null)
                    criticas.Append("Faixa de valor inválida|");
            }

            // Verificando Campanha
            if (dto.Campanha != null)
            {
                Campanha campanha = _campanhaDomain.ObterPorId(dto.Campanha.Id);
                if (campanha == null)
                    criticas.Append("Campanha inválida|");
                else
                    if (campanha.DataFinal < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                        criticas.Append("Campanha expirada|");
            }

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

                if (dto.ValorFaixa != null)
                    entidade.ValorFaixaId = dto.ValorFaixa.Id;

                if (dto.Campanha != null)
                    entidade.CampanhaId = dto.Campanha.Id;

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
        /// <param name="mensagem">Variável string de saída de dados (mensagem)</param>
        public void Atualizar(ItemDto dto, out int statusCode, out string mensagem)
        {


            StringBuilder criticas = new StringBuilder();

            // Verificando Item
            Item entidade = _dmn.ObterPorId(dto.Id);
            if (entidade == null)
            {
                mensagem = "Item não encontrado";
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {

                // Verificando TipoItem
                TipoItem tipoItem = _tipoItemDomain.ObterPorDescricao(dto.TipoItem.Descricao);
                if (tipoItem == null)
                    criticas.Append("Tipo de Item inválido|");


                // Verificando Categoria
                Categoria categoria = _categoriaDomain.ObterPorId(dto.Categoria.Id);
                if (categoria == null)
                    criticas.Append("Categoria inválida|");

                // Verificando faixa de valor
                if (dto.ValorFaixa != null)
                {
                    ValorFaixa faixa = _faixaDomain.ObterPorId(dto.ValorFaixa.Id);
                    if (faixa == null)
                        criticas.Append("Faixa de valor inválida|");
                }

                // Verificando Campanha
                if (dto.Campanha != null)
                {
                    Campanha campanha = _campanhaDomain.ObterPorId(dto.Campanha.Id);
                    if (campanha == null)
                        criticas.Append("Campanha inválida|");
                    else
                        if (campanha.DataFinal < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                        criticas.Append("Campanha expirada|");
                }

                if (criticas.Length > 0)
                {
                    mensagem = criticas.ToString().Substring(0, (criticas.Length - 1)).Replace("|", "\r\n");
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

                    if (dto.ValorFaixa == null)
                        entidade.ValorFaixaId = null;
                    else
                        entidade.ValorFaixaId = dto.ValorFaixa.Id;

                    if (dto.Campanha == null)
                        entidade.CampanhaId = null;
                    else
                        entidade.CampanhaId = dto.Campanha.Id;

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

                Task.Factory.StartNew(() =>
                {
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
        /// Executar a pesquisa de itens livres para match com base nos critérios
        /// </summary>
        /// <param name="dataInicial">Data inicial do período</param>
        /// <param name="dataFinal">Data final do período</param>
        /// <param name="categoriaId">Id da categoria</param>
        public IEnumerable<ItemDto> ListarLivresParaMatches(DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId)
        {
            IEnumerable<Item> itens = _dmn.PesquisarParaMatches(dataInicial, dataFinal, categoriaId);
            CarregaRelacoes(itens);
            return ConverterEntidadeEmDto(itens);
        }

        /// <summary>
        /// Listar os matches realizados com base nos filtros informados
        /// </summary>
        /// <param name="dataInicial">Data inicial do período</param>
        /// <param name="dataFinal">Data final do período</param>
        /// <param name="categoriaId">Id da categoria</param>
        /// <param name="efetivados">Boolean indicando se é para listar efetivados ou não efetivados (null = lista todos)</param>
        public IEnumerable<ItemMatchReportDto> ListarMatches(DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId, bool? efetivados)
        {
            return _dmn.ListarMatches(dataInicial, dataFinal, categoriaId, efetivados);
        }

        /// <summary>
        /// Obter o ranking individual de docações
        /// </summary>
        public IEnumerable<RankingIndividualReportDto> RankingIndividual() => _dmn.RankingIndividual();

        /// <summary>
        /// Listar os matches realizados do usuário logado
        /// </summary>
        public void ListarMatches(Guid usuarioId, out int statusCode, out object dadosRetorno)
        {

            try
            {
                statusCode = StatusCodes.Status200OK;
                dadosRetorno = new
                {
                    Sucesso = true,
                    Mensagem = _dmn.ListarMatches(usuarioId)
                };
            }
            catch (Exception ex)
            {
                statusCode = StatusCodes.Status500InternalServerError;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"Falha ao executar operação [{ex.Message} - {ex.StackTrace}]"
                };
            }
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

        /// <summary>
        /// Efetua o match entre item de doação e item de necessidade
        /// </summary>
        /// <param name="doacaoId">Id do item de doação</param>
        /// <param name="necessidadeId">Id do item de necessidade</param>
        /// <param name="statusCode">Variável de saída StatusCode de resposta</param>
        /// <param name="dadosRetorno">Variável de saída de mensagem de retorno</param>
        public void ExecutarMatch(Guid doacaoId, Guid necessidadeId, out int statusCode, out object dadosRetorno)
        {

            // Validando item de necessidade
            Item itemNecessidade = _dmn.ObterPorId(necessidadeId);
            if (itemNecessidade == null)
            {
                statusCode = StatusCodes.Status400BadRequest;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"O item de necessidade '{necessidadeId.ToString()}' não foi encontrado"
                };
                return;
            }
            else
            {
                CarregaRelacoes(itemNecessidade);
                if (!itemNecessidade.TipoItem.Descricao.ToLower().Equals("necessidade"))
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    dadosRetorno = new
                    {
                        Sucesso = false,
                        Mensagem = $"O item de necessidade '{necessidadeId.ToString()}' não é do tipo 'Necessidade'"
                    };
                    return;
                }
            }

            // Validando item de doação
            Item itemDoacao = _dmn.ObterPorId(doacaoId);
            if (itemDoacao == null)
            {
                statusCode = StatusCodes.Status400BadRequest;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"O item de doação '{doacaoId.ToString()}' não foi encontrado"
                };
                return;
            }
            else
            {
                CarregaRelacoes(itemDoacao);
                if (!itemDoacao.TipoItem.Descricao.ToLower().Equals("doação"))
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    dadosRetorno = new
                    {
                        Sucesso = false,
                        Mensagem = $"O item de doação'{doacaoId.ToString()}' não é do tipo 'Doação'"
                    };
                    return;
                }
            }

            // Verificando se o match já existe
            ItemMatch match = _matchDomain.BuscarPorMatch(doacaoId, necessidadeId);
            if (match != null)
            {
                statusCode = StatusCodes.Status400BadRequest;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"Já existe um match para esses itens"
                };
                return;
            }

            // TODO: Informar valor faixa
            // Gravando o match
            match = new ItemMatch()
            {
                UsuarioId = _usuario.Id,
                DoacaoId = doacaoId,
                NecessidadeId = necessidadeId,
                TipoMatchId = Guid.Parse("a3412363-d87d-11e8-abfa-0e0e947bb2d6"),
                ValorFaixaId = null,
                Efetivado = true
            };
            _matchDomain.Adicionar(match);
            _uow.Efetivar();

            statusCode = StatusCodes.Status200OK;
            dadosRetorno = new
            {
                Sucesso = true,
                Mensagem = $"Match realizado com sucesso",
                Id = match.Id.ToString()
            };

        }

        /// <summary>
        /// Desfaz um match realizado
        /// </summary>
        /// <param name="id">Id do match</param>
        /// <param name="statusCode">Variável de saída StatusCode de resposta</param>
        /// <param name="dadosRetorno">Variável de saída de mensagem de retorno</param>
        public void DesfazerMatch(Guid id, out int statusCode, out object dadosRetorno)
        {

            // Validando match
            ItemMatch match = _matchDomain.ObterPorId(id);
            if (match == null)
            {

                statusCode = StatusCodes.Status400BadRequest;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"O match de id {id.ToString()} não foi encontrado"
                };

            }
            else
            {
                _matchDomain.Excluir(id);
                _uow.Efetivar();

                statusCode = StatusCodes.Status200OK;
                dadosRetorno = new
                {
                    Sucesso = true,
                    Mensagem = $"Match excluído com sucesso"
                };

            }

        }

        public void ExecutarMatch(Guid id, Guid? valorFaixaId, out int statusCode, out object dadosRetorno)
        {

            // Buscando item
            Item itemAlvo = _dmn.ObterPorId(id);
            if (itemAlvo == null)
            {
                statusCode = StatusCodes.Status400BadRequest;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"O item '{id.ToString()}' não foi encontrado"
                };
                return;
            }
            CarregaRelacoes(itemAlvo);

            if (itemAlvo.TipoItemId.Equals(new Guid("0acd2bb5-c5a5-11e8-ab80-0242ac110006")))
            {

                // Se o item alvo for doação,
                // então está sendo solicitado o item, 
                // então deve-se informar o valor
                // pois será GERADO UMA NECESSIDADE

                if (valorFaixaId == null)
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    dadosRetorno = new
                    {
                        Sucesso = false,
                        Mensagem = "Um valor (não negativo) deve ser informado para match de item de necessidade"
                    };
                    return;
                }
            }
            else
            {

                // Se o item alvo for necessidade,
                // então capturar o valor dessa necessidade
                valorFaixaId = itemAlvo.ValorFaixaId;

            }

            // Verificando se existe match para o Item
            ItemMatch match = _matchDomain.Obter(x => x.DoacaoId.Equals(id) || x.NecessidadeId.Equals(id)).FirstOrDefault();
            if (match != null)
            {
                statusCode = StatusCodes.Status400BadRequest;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"Já existe match para esse item"
                };
                return;
            }

            // Buscando tipo de item relacionado
            string tipoItemOpostoDescricao = itemAlvo.TipoItem.Descricao.ToLower().Equals("necessidade") ? "Doação" : "Necessidade";

            Guid? valorFaixaIdOposta = null;
            if (itemAlvo.TipoItem.Descricao.ToLower().Equals("necessidade"))
                valorFaixaIdOposta = null;
            else
                valorFaixaIdOposta = valorFaixaId.Value;

            TipoItem tipoItemOposto = _tipoItemDomain.ObterPorDescricao(tipoItemOpostoDescricao);
            if (tipoItemOposto == null)
            {
                statusCode = StatusCodes.Status500InternalServerError;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = "Falha ao identificar tipo de item oposto"
                };
                return;
            }

            // Criando item oposto (relacionado)
            Item itemOposto = new Item()
            {
                Titulo = $"[{tipoItemOpostoDescricao.ToUpper()}] {itemAlvo.Titulo}",
                Descricao = itemAlvo.Descricao,
                TipoItemId = tipoItemOposto.Id,
                CategoriaId = itemAlvo.CategoriaId,
                UsuarioId = _usuario.Id,
                Anonimo = itemAlvo.Anonimo,
                ValorFaixaId = valorFaixaIdOposta,
                GeradoPorMatch = true
            };

            _dmn.Adicionar(itemOposto);

            // Gravando match
            Guid doacaoId = tipoItemOpostoDescricao.ToLower().Equals("doação") ? itemOposto.Id : itemAlvo.Id;
            Guid necessidadeId = tipoItemOpostoDescricao.ToLower().Equals("necessidade") ? itemOposto.Id : itemAlvo.Id;
            Guid tipoMatchId = Guid.Parse(tipoItemOpostoDescricao.ToLower().Equals("necessidade") ? "b69eed41-d87c-11e8-abfa-0e0e947bb2d6" : "b69eed4f-d87c-11e8-abfa-0e0e947bb2d6");

            match = new ItemMatch()
            {
                DoacaoId = doacaoId,
                NecessidadeId = necessidadeId,
                UsuarioId = _usuario.Id,
                TipoMatchId = tipoMatchId,
                ValorFaixaId = valorFaixaId,
                Efetivado = !itemAlvo.Anonimo
            };
            _matchDomain.Adicionar(match);

            try
            {

                _uow.Efetivar();
                statusCode = StatusCodes.Status200OK;
                dadosRetorno = new
                {
                    Sucesso = true,
                    Mensagem = new
                    {
                        MatchId = match.Id,
                        ItemRelacaoId = itemOposto.Id.ToString()
                    }
                };

            }
            catch (Exception ex)
            {
                statusCode = StatusCodes.Status500InternalServerError;
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"Falha na operação - [{ex.Message} - {ex.StackTrace}]"
                };
            }

        }

        public void EfetivarMatch(Guid matchId, out int statusCode, out string mensagem)
        {

            if (!_usuario.Perfis.Contains("Admin"))
            {
                statusCode = StatusCodes.Status401Unauthorized;
                mensagem = "Ação permitida apenas para administradores";
            }
            else
            {

                ItemMatch match = _matchDomain.ObterPorId(matchId);
                if (match == null)
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    mensagem = $"O match de id '{matchId}' não foi localizado";
                }
                else
                {
                    if (match.Efetivado)
                    {
                        statusCode = StatusCodes.Status400BadRequest;
                        mensagem = $"Esse match já foi efetivado";
                    }
                    else
                    {
                        try
                        {
                            match.Efetivado = true;
                            _matchDomain.EfetivarMatch(match);
                            _uow.Efetivar();
                            statusCode = StatusCodes.Status200OK;
                            mensagem = "Match efetivado com sucesso";
                        }
                        catch (Exception ex)
                        {
                            statusCode = StatusCodes.Status500InternalServerError;
                            mensagem = $"Falha na operação de efetivação do match [{ex.Message} - {ex.StackTrace}]";
                        }
                    }
                }

            }

        }

        #endregion

    }
}
