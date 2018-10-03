using SantaHelena.ClickDoBem.Application.Dto;
using SantaHelena.ClickDoBem.Services.Api.Model.Request.Bases;
using SantaHelena.ClickDoBem.Services.Api.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros
{

    /// <summary>
    /// Modelo de requisição de inserção de Colaborador
    /// </summary>
    public class ColaboradorInsertRequest : ViewModelBase
    {

        /// <summary>
        /// Número do documento (Cpf/Cnpj)
        /// </summary>
        [Required(ErrorMessage = "O documento deve ser informado")]
        [CpfCnpjValidation]
        public string Documento { get; set; }

        /// <summary>
        /// Nome do colaborador
        /// </summary>
        [Required(ErrorMessage = "O nome deve ser informado")]
        [StringLength(254, MinimumLength = 5, ErrorMessage = "O nome deve conter entre 3 e 150 caracteres")]
        public string Nome { get; set; }

        /// <summary>
        /// Data de nascimento (formato YYYY-MM-AA)
        /// </summary>
        [Required(ErrorMessage = "A data de nascimento deve ser informada")]
        public DateTime? DataNascimento { get; set; }

        /// <summary>
        /// Endereço do colaborador
        /// </summary>
        public EnderecoRequest Endereco { get; set; }

        /// <summary>
        /// Número do telefone fixo
        /// </summary>
        [MaxLength(20, ErrorMessage = "O telefone fixo deve conter no máximo 20 caracteres")]
        public string TelefoneFixo { get; set; }

        /// <summary>
        /// Número do telefone fixo
        /// </summary>
        [Required(ErrorMessage = "O telefone celular deve ser informado")]
        [MaxLength(20, ErrorMessage = "O telefone celular deve conter no máximo 20 caracteres")]
        public string TelefoneCelular { get; set; }

        /// <summary>
        /// E-mail do colaborador/usuário
        /// </summary>
        [Required(ErrorMessage = "O e-mail deve ser informado")]
        [MaxLength(1000, ErrorMessage = "O e-mail deve conter no máximo 1000 caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário (puro texto)
        /// </summary>
        [Required(ErrorMessage = "A senha deve ser informada")]
        [SenhaValidation("DataNascimento")]
        public string Senha { get; set; }

    }
}
