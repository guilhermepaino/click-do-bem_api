using SantaHelena.ClickDoBem.Application.Dto;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais
{

    /// <summary>
    /// Request de Autenticacao
    /// </summary>
    public class AutenticacaoRequest
    {

        /// <summary>
        /// Nome do usuário (UserName)
        /// </summary>
        [Required(ErrorMessage = "O nome deve ser informado")]
        [MaxLength(50, ErrorMessage = "O nome deve conter no máximo 150 caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Senha do usuário (Hash Md5)
        /// </summary>
        [Required(ErrorMessage = "A senha deve ser informada (HashMD5).")]
        [StringLength(32, ErrorMessage = "O hash de senha deve ter 32 caracteres.")]
        public string Senha { get; set; }

    }
}
