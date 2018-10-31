using System;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Modelo de requisição de listagem de matches
    /// </summary>
    public class ListagemMatchRequest
    {

        /// <summary>
        /// Data inicial do período
        /// </summary>
        public DateTime? DataInicial { get; set; }

        /// <summary>
        /// Data final do período
        /// </summary>
        public DateTime? DataFinal { get; set; }

        /// <summary>
        /// Id da categoria
        /// </summary>
        public Guid? CategoriaId { get; set; }

        /// <summary>
        /// Flag indicando se serão exibidos matches efetivados ou não (null = exibe todos)
        /// </summary>
        public bool? Efetivados { get; set; }

    }

}
