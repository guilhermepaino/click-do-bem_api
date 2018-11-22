using SantaHelena.ClickDoBem.Domain.Core;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.IO;

namespace SantaHelena.ClickDoBem.Domain.Services.Cadastros
{
    public class CampanhaDomainService : DomainServiceBase<Campanha, ICampanhaRepository>, ICampanhaDomainService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly ICampanhaImagemDomainService _imgDomain;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do domínio
        /// </summary>
        /// <param name="repository">Contexto de banco de dados</param>
        public CampanhaDomainService(ICampanhaRepository repository, ICampanhaImagemDomainService imgDomain, IUnitOfWork uow) : base(repository)
        {
            _imgDomain = imgDomain;
            _uow = uow;
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Buscar Campanha pela descrição (igualdade)
        /// </summary>
        /// <param name="descricao">Descrição a ser localizada</param>
        /// <returns></returns>
        public Campanha ObterPorDescricao(string descricao) => _repository.ObterPorDescricao(descricao);

        /// <summary>
        /// Buscar Campanhas por semelhança (descrição)
        /// </summary>
        /// <param name="descricao">Descrição a ser filtrada</param>
        /// <returns></returns>
        public IEnumerable<Campanha> ObterPorSemelhanca(string descricao) => _repository.ObterPorSemelhanca(descricao);

        /// <summary>
        /// Encerrar uma campanha imediatamente
        /// </summary>
        public void EncerrarCampanha(Guid id) => _repository.EncerrarCampanha(id);

        public bool CarregarImagem(Campanha campanha, string imagemBase64, string caminho, out object dadosRetorno)
        {

            // Gerar arquivo
            CampanhaImagem imagem = new CampanhaImagem()
            {
                CampanhaId = campanha.Id
            };
            string arquivo = $"{campanha.Id.ToString()}.jpg";
            string nomeCompleto = Path.Combine(caminho, arquivo);
            string caminhoUrl = $"/images/campanha/{campanha.Id.ToString()}.jpg";
            imagem.Caminho = caminhoUrl;

            try
            {
                if (!Directory.Exists(caminho))
                    Directory.CreateDirectory(caminho);
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

                if (File.Exists(nomeCompleto))
                    File.Delete(nomeCompleto);

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

                CampanhaImagem img = _imgDomain.ObterPorCampanha(campanha.Id);
                if (img == null)
                {
                    _imgDomain.Adicionar(imagem);
                    _uow.Efetivar();
                }
                else
                {
                    imagem = img;
                }

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

        #endregion

    }
}
