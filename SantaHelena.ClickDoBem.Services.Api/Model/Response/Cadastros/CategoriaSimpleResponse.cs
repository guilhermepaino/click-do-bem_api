using System;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Cadastros
{

    /// <summary>
    /// Objeto de resposta de Api de Categoria
    /// </summary>
    public class CategoriaSimpleResponse
    {

        /// <summary>
        /// Id da categoria
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descriçaõ da categoria
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Pontuação da categoria
        /// </summary>
        public int Pontuacao { get; set; }

        /// <summary>
        /// Flag de indicação de geranciada pelo Rh
        /// </summary>
        public bool GerenciadaRh { get; set; }

    }
}
