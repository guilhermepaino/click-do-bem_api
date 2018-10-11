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
        /// Obter registros de necessidades
        /// </summary>
        public IEnumerable<Item> ObterNecessidades() => _repository.ObterNecessidades();

        /// <summary>
        /// Obter registros de doações
        /// </summary>
        public IEnumerable<Item> ObterDoacoes() => _repository.ObterDoacoes();

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
            //TODO: NotImplementedException

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
            string arquivo = $"{imagem.Id.ToString()}.png";
            string nomeCompleto = Path.Combine(pastaItem, arquivo);
            string caminhoUrl = $"/images/{item.Id.ToString()}/{imagem.Id.ToString()}.png";
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

            byte[] bytes = Convert.FromBase64String(imagemBase64);
            File.WriteAllBytes(nomeCompleto, bytes);

            try
            {

                _imgDomain.Adicionar(imagem);
                _uow.Efetivar();

                dadosRetorno = new
                {
                    Sucesso = true,
                    Mensagem = "Imagem carregada com sucesso",
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
                    Mensagem = $"Falha ao gravar registro - {ex.Message} - {ex.StackTrace}",
                    Arquivo = caminhoUrl
                };

            }

            return false;

        }

        #endregion

    }
}
