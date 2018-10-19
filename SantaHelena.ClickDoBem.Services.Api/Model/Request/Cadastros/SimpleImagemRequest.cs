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
        public string ImagemBase64 { get; set; }

    }

}
