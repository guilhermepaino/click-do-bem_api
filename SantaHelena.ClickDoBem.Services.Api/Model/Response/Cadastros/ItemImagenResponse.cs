namespace SantaHelena.ClickDoBem.Services.Api.Model.Response.Cadastros
{

    /// <summary>
    /// Objeto de resposta de imagem de item
    /// </summary>
    public class ItemImagenResponse
    {

        /// <summary>
        /// Nome (título da imagem)
        /// </summary>
        public string NomeImagem { get; set; }

        /// <summary>
        /// Arquivo da imagem (caminho relativo)
        /// </summary>
        public string Arquivo { get; set; }

    }
}
