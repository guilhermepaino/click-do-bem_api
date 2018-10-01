using System;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de INSERT de item
    /// </summary>
    public class ItemInsertRequest
    {

        /// <summary>
        /// Título do item (obrigatório)
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição do item (obrigatório)
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Id do tipo de item (obrigatório)
        /// </summary>
        public string TipoItem { get; set; }

        /// <summary>
        /// Id da categoria (obrigatório)
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Flag indicando se o item deve ser anônimo
        /// </summary>
        public bool Anonimo { get; set; }

    }

}
