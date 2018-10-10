using SantaHelena.ClickDoBem.Application.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de Filtro de Itens
    /// </summary>
    public class PesquisaItemRequest
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
        /// Id do tipo do item
        /// </summary>
        public Guid? TipoItemId { get; set; }

        /// <summary>
        /// Id da categoria
        /// </summary>
        public Guid? CategoriaId { get; set; }

    }

}
