using SantaHelena.ClickDoBem.Application.Dto;
using SantaHelena.ClickDoBem.Services.Api.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de INSERT de item
    /// </summary>
    public class ItemInsertRequest : ViewModelBase
    {

        /// <summary>
        /// Título do item (obrigatório)
        /// </summary>
        [Required(ErrorMessage = "O título deve ser informado.")]
        [MaxLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição do item (obrigatório)
        /// </summary>
        [Required(ErrorMessage = "A descrição deve ser informada.")]
        [MaxLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
        public string Descricao { get; set; }

        /// <summary>
        /// Id do tipo de item (obrigatório)
        /// </summary>
        [Required(ErrorMessage = "O tipo de item deve ser informado.")]
        [Range(1, 2, ErrorMessage = "Deve ser informado 1=Necessidade ou 2=Doação")]
        public int? TipoItem { get; set; }

        /// <summary>
        /// Id da categoria (obrigatório)
        /// </summary>
        [Required(ErrorMessage = "O Id da categoria deve ser informado")]
        public Guid CategoriaId { get; set; }

        /// <summary>
        /// Flag indicando se o item deve ser anônimo
        /// </summary>
        public bool Anonimo { get; set; }

        /// <summary>
        /// Id da faixa de valor estimado do item (obrigatório para necessidades)
        /// </summary>
        public Guid? ValorFaixaId { get; set; }

        /// <summary>
        /// Lista de Imagens do item
        /// </summary>
        [ImagensValidation]
        public IEnumerable<SimpleImagemRequest> Imagens { get; set; }

    }

}
