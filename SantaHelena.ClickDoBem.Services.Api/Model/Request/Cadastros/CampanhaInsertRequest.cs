using SantaHelena.ClickDoBem.Application.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de INSERT de Campanha
    /// </summary>
    public class CampanhaInsertRequest : ViewModelBase
    {

        /// <summary>
        /// Descrição da Campanha (obrigatório)
        /// </summary>
        [Required(ErrorMessage = "A descrição deve ser informada.")]
        [MaxLength(1000, ErrorMessage = "A descrição deve ter no máximo 150 caracteres.")]
        public string Descricao { get; set; }

        /// <summary>
        /// Data inicial da campanha
        /// </summary>
        [Required(ErrorMessage = "A data inicial da campanha deve ser informada.")]
        public DateTime? DataInicial { get; set; }

        /// <summary>
        /// Data final da campanha
        /// </summary>
        [Required(ErrorMessage = "A data final da campanha deve ser informada.")]
        public DateTime? DataFinal { get; set; }

        /// <summary>
        /// Prioridade da campanha (0=baixa / 1=Normal / 2 = Alta / 3 = Altíssima)
        /// </summary>
        [Required(ErrorMessage = "A data final da campanha deve ser informada.")]
        public int Prioridade { get; set; }

    }

}
