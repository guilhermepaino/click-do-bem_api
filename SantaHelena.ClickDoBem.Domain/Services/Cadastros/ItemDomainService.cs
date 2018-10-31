using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Core.ReportDto;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class ItemDomainService : DomainServiceBase<Item, IItemRepository>, IItemDomainService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly IItemImagemDomainService _imgDomain;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public ItemDomainService
        (
            IUnitOfWork uow,
            IItemRepository repository,
            IItemImagemDomainService imgDomain
        ) : base(repository)
        {
            _uow = uow;
            _imgDomain = imgDomain;
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        /// <param name="incluirMatches">Flag indicando se vai incluir os itens de matches</param>
        public IEnumerable<Item> ObterTodos(bool incluirMatches) => _repository.ObterTodos(incluirMatches);

        /// <summary>
        /// Obter registros de necessidades
        /// </summary>
        /// <param name="incluirMatches">Flag indicando se vai incluir os itens de matches</param>
        public IEnumerable<Item> ObterNecessidades(bool incluirMatches) => _repository.ObterNecessidades(incluirMatches);

        /// <summary>
        /// Obter registros de doações
        /// </summary>
        /// <param name="incluirMatches">Flag indicando se vai incluir os itens de matches</param>
        public IEnumerable<Item> ObterDoacoes(bool incluirMatches) => _repository.ObterDoacoes(incluirMatches);

        /// <summary>
        /// Pesquisar itens com base nos filtros informados
        /// </summary>
        /// <param name="dataInicial">Data inicial do período</param>
        /// <param name="dataFinal">Data final do período</param>
        /// <param name="tipoItemId">Id do tipo de item</param>
        /// <param name="categoriaId">Id da categoria</param>
        public IEnumerable<ItemListaReportDto> Pesquisar(DateTime? dataInicial, DateTime? dataFinal, Guid? tipoItemId, Guid? categoriaId)
            => _repository.Pesquisar(dataInicial, dataFinal, tipoItemId, categoriaId);

        /// <summary>
        /// Listar os matches realizados com base nos filtros informados
        /// </summary>
        /// <param name="dataInicial">Data inicial do período</param>
        /// <param name="dataFinal">Data final do período</param>
        /// <param name="categoriaId">Id da categoria</param>
        /// <param name="efetivados">Boolean indicando se é para listar efetivados ou não efetivados (null = lista todos)</param>
        public IEnumerable<ItemMatchReportDto> ListarMatches(DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId, bool? efetivados)
            => _repository.ListarMatches(dataInicial, dataFinal, categoriaId, efetivados);

        /// <summary>
        /// Pesquisar itens livres para matches com base nos filtros informados
        /// </summary>
        /// <param name="dataInicial">Data inicial do período</param>
        /// <param name="dataFinal">Data final do período</param>
        /// <param name="tipoItemId">Id do tipo de item</param>
        /// <param name="categoriaId">Id da categoria</param>
        public IEnumerable<Item> PesquisarParaMatches(DateTime? dataInicial, DateTime? dataFinal, Guid? categoriaId)
            => _repository.PesquisarParaMatche(dataInicial, dataFinal, categoriaId);

        /// <summary>
        /// Carrega uma imagem para um item
        /// </summary>
        /// <param name="item">Item para o qual a imagem será anexada</param>
        /// <param name="nomeImagem">Nome da imagem</param>
        /// <param name="imagemBase64">Expressão Base64 da imagem</param>
        /// <param name="caminho">Caminho onde o arquivo será gravado</param>
        /// <param name="dadosRetorno">Varíavel (object) de saída da resposta</param>
        public bool CarregarImagem(Item item, string nomeImagem, string imagemBase64, string caminho, out object dadosRetorno)
        {

            // Gerar arquivo
            string pastaItem = Path.Combine(caminho, item.Id.ToString());

            IEnumerable<ItemImagem> imagens = _imgDomain.Obter(x => x.ItemId.Equals(item.Id));

            if (imagens.Count().Equals(5))
            {
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = "Número máximo de imagens atingido"
                };
                return false;
            }

            ItemImagem imagem = new ItemImagem()
            {
                ItemId = item.Id,
                NomeOriginal = nomeImagem
            };
            string arquivo = $"{imagem.Id.ToString()}.jpg";
            string nomeCompleto = Path.Combine(pastaItem, arquivo);
            string caminhoUrl = $"/images/{item.Id.ToString()}/{imagem.Id.ToString()}.jpg";
            imagem.Caminho = caminhoUrl;

            try
            {
                if (!Directory.Exists(pastaItem))
                    Directory.CreateDirectory(pastaItem);
            }
            catch (Exception ex)
            {
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"Falha ao acessar local de armazenamento do arquivo - {ex.Message} - {ex.StackTrace}"
                };
                return false;
            }

            try
            {
                byte[] bytes = Convert.FromBase64String(imagemBase64);
                File.WriteAllBytes(nomeCompleto, bytes);
            }
            catch (Exception ex)
            {
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"Falha ao converter Base64 em Imagem - {ex.Message} - {ex.StackTrace}"
                };
                return false;
            }

            try
            {

                _imgDomain.Adicionar(imagem);
                _uow.Efetivar();

                dadosRetorno = new
                {
                    Sucesso = true,
                    Mensagem = "Imagem carregada com sucesso",
                    Id = imagem.Id.ToString(),
                    Arquivo = caminhoUrl
                };

                return true;

            }
            catch (Exception ex)
            {

                File.Delete(nomeCompleto);
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"Falha ao gravar registro - {ex.Message} - {ex.StackTrace}"
                };

            }

            return false;

        }

        /// <summary>
        /// Remover uma imagem de um item
        /// </summary>
        /// <param name="id">Id da imagem</param>
        /// <param name="dadosRetorno">Varíavel (object) de saída da resposta</param>
        public bool RemoverImagem(Guid id, string caminho, out object dadosRetorno)
        {

            try
            {

                ItemImagem imagem = _imgDomain.ObterPorId(id);

                if (imagem == null)
                {
                    dadosRetorno = new
                    {
                        Sucesso = false,
                        Mensagem = $"Registro de imagem não encontrado"
                    };
                    return false;
                }

                string nomeCompleto = Path.Combine(caminho, imagem.ItemId.ToString(), $"{imagem.Id.ToString()}{imagem.Caminho.Substring(imagem.Caminho.LastIndexOf("."))}");
                if (!Directory.Exists(caminho))
                {
                    dadosRetorno = new
                    {
                        Sucesso = false,
                        Mensagem = $"Caminho do arquivo da imagem não encontrado"
                    };
                    return false;
                }
                else
                {
                    if (!File.Exists(nomeCompleto))
                    {
                        dadosRetorno = new
                        {
                            Sucesso = false,
                            Mensagem = $"Arquivo da imagem não encontrado"
                        };
                        return false;
                    }
                    else
                    {
                        File.Delete(nomeCompleto);
                        _imgDomain.Excluir(id);
                        _uow.Efetivar();
                        dadosRetorno = new
                        {
                            Sucesso = true,
                            Mensagem = $"Imagem removida com sucesso"
                        };
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                dadosRetorno = new
                {
                    Sucesso = false,
                    Mensagem = $"Falha ao remover a imagem - {ex.Message} - {ex.StackTrace}"
                };
                return false;
            }

        }

        #endregion

    }
}
