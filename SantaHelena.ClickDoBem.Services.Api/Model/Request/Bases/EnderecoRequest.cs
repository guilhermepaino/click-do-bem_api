using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Services.Api.Model.Request.Bases
{

    /// <summary>
    /// Modelo base de endereço
    /// </summary>
    public class EnderecoRequest
    {

        /// <summary>
        /// Logradouro do endereço (rua, avenida, etc)
        /// </summary>
        [Required(ErrorMessage = "O logradouro deve ser informado")]
        [MaxLength(100, ErrorMessage = "O logradouro deve conter no máximo 100 caracteres")]
        public string Logradouro { get; set; }

        /// <summary>
        /// Número do endereço
        /// </summary>
        [Required(ErrorMessage = "O número deve ser informado")]
        [MaxLength(30, ErrorMessage = "O número deve conter no máximo 30 caracteres")]
        public string Numero { get; set; }

        /// <summary>
        /// Complemento do endereço (bloco, apto, andar, etc)
        /// </summary>
        [MaxLength(50, ErrorMessage = "O complemento deve conter no máximo 50 caracteres")]
        public string Complemento { get; set; }

        /// <summary>
        /// Bairro do endereço
        /// </summary>
        [Required(ErrorMessage = "O bairro deve ser informado")]
        [MaxLength(80, ErrorMessage = "O bairro deve conter no máximo 80 caracteres")]
        public string Bairro { get; set; }

        /// <summary>
        /// Cidade do endereço
        /// </summary>
        [Required(ErrorMessage = "A cidade deve ser informado")]
        [MaxLength(100, ErrorMessage = "A cidade deve conter no máximo 100 caracteres")]
        public string Cidade { get; set; }

        /// <summary>
        /// Uf do endereço
        /// </summary>
        [Required(ErrorMessage = "A Uf deve ser informada")]
        [StringLength(2, ErrorMessage = "A uf deve conter 2 posições")]
        public string Uf { get; set; }

        /// <summary>
        /// CEP do endereço (apenas números)
        /// </summary>
        [Required(ErrorMessage = "O Cep deve ser informado")]
        [StringLength(8, ErrorMessage = "O Cep deve conter 8 dígitos")]
        public string Cep { get; set; }

    }
}
