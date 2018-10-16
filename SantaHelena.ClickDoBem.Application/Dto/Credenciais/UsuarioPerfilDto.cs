using SantaHelena.ClickDoBem.Application.Interfaces;

namespace SantaHelena.ClickDoBem.Application.Dto.Credenciais
{
    public class UsuarioPerfilDto : IAppDto
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do Dto
        /// </summary>
        public UsuarioPerfilDto() { }

        /// <summary>
        /// Cria uma nova instância do Dto
        /// </summary>
        /// <param name="perfil"></param>
        public UsuarioPerfilDto(string perfil)
        {
            Perfil = perfil;
        }

        #endregion

        #region Propriedades

        public string Perfil { get; set; }

        #endregion

    }
}
