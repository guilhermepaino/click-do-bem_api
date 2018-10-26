using SantaHelena.ClickDoBem.Application.Dto;
using SantaHelena.ClickDoBem.Services.Api.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Credenciais
{

    /// <summary>
    /// Modelo de request de troca de senha
    /// </summary>
    public class EsqueciSenha : ViewModelBase
    {

        /// <summary>
        /// Número do documento do usuário (Cpf ou Cnpj)
        /// </summary>
        [Required(ErrorMessage = "O número do documento deve ser informado")]
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Data de Nascimento do usuário
        /// </summary>
        [Required(ErrorMessage = "A data de nascimento deve ser informada")]
        public DateTime? DataNascimento { get; set; }

        /// <summary>
        /// Nova Senha
        /// </summary>
        [Required(ErrorMessage = "A senha deve ser informada")]
        [SenhaValidation("DataNascimento")]
        public string NovaSenha { get; set; }

        /// <summary>
        /// Confirmação de senha
        /// </summary>
        [Required(ErrorMessage = "A confirmação de senha deve ser informada")]
        [ConfirmacaoSenhaValidation("NovaSenha")]
        public string ConfirmarSenha { get; set; }

    }
}
