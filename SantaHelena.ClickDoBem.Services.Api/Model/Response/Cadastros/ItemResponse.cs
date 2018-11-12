using SantaHelena.ClickDoBem.Services.Api.Model.Response.Credenciais;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Cadastros
{

    /// <summary>
    /// Objeto de resposta de item
    /// </summary>
    public class ItemResponse
    {

        /// <summary>
        /// Id do item
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Data de inclusão do item
        /// </summary>
        public DateTime DataInclusao { get; set; }

        /// <summary>
        /// Data da última alteraçaõ do item
        /// </summary>
        public DateTime? DataAlteracao { get; set; }

        /// <summary>
        /// Título do item
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição do item
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Tipo do item
        /// </summary>
        public string TipoItem { get; set; }

        /// <summary>
        /// Categoria do item
        /// </summary>
        public CategoriaSimpleResponse Categoria { get; set; }

        /// <summary>
        /// Usuário que fez o registro do item
        /// </summary>
        public UsuarioSimpleResponse Usuario { get; set; }

        /// <summary>
        /// Flag indicando que o usúario quer ser anônimo
        /// </summary>
        public bool Anonimo { get; set; }

        /// <summary>
        /// ValorFaixa estimado do item
        /// </summary>
        public ValorFaixaSimpleResponse ValorFaixa { get; set; }

        /// <summary>
        /// Lista das imagens do item
        /// </summary>
        public IEnumerable<ItemImagenResponse> Imagens { get; set; }

    }
}
