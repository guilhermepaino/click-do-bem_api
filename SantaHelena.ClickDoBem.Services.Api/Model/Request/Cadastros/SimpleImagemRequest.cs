namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Request de Upload de Imagens
    /// </summary>
    public class SimpleImagemRequest
    {

        /// <summary>
        /// Nome (Título da Imagem)
        /// </summary>
        public string NomeImagem { get; set; }

        /// <summary>
        /// Expressão string do arquivo (Base64)
        /// </summary>
        /// <seealso cref="https://pt.wikipedia.org/wiki/Base64"/>
        /// <seealso cref="https://www.4devs.com.br/codificar_decodificar_base64"/>
        public string ImagemBase64 { get; set; }

    }

}
