using SantaHelena.ClickDoBem.Application.Dto;
using SantaHelena.ClickDoBem.Services.Api.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais
{

    /// <summary>
    /// Modelo de request de troca de senha
    /// </summary>
    public class TrocaSenhaRequest : ViewModelBase
    {

        /// <summary>
        /// Senha atual (MD5)
        /// </summary>
        [Required(ErrorMessage = "A senha atual deve ser informada")]
        public string SenhaAtual { get; set; }

        /// <summary>
        /// Nova Senha
        /// </summary>
        [Required(ErrorMessage = "A senha deve ser informada")]
        [SenhaValidation]
        public string NovaSenha { get; set; }

        /// <summary>
        /// Confirmação de senha
        /// </summary>
        [Required(ErrorMessage = "A confirmação de senha deve ser informada")]
        [ConfirmacaoSenhaValidation("NovaSenha")]
        public string ConfirmarSenha { get; set; }

    }
}
