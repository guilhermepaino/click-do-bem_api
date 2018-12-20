using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SantaHelena.ClickDoBem.Domain.Core.Tools;

namespace SantaHelena.ClickDoBem.Services.Api.Validations
{

    /// <summary>
    /// Validador de complexidade de senha
    /// </summary>
    public class SenhaValidationAttribute : ValidationAttribute
    {

        #region Objeto/Variáveis Locais

        /// <summary>
        /// Nome da propriedade de data de nascimento
        /// </summary>
        protected readonly string _nomePropriedadeDataNascimento;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do atributo
        /// </summary>
        public SenhaValidationAttribute() : base("A senha não atende aos requisitos de complexidade.\r\n\r\nPossuir entre 6 caracteres e 8 caracteres, não poderá ser igual a data de nascimento, pode conter apenas letras e números")
        {
        }

        /// <summary>
        /// Cria uma nova instância do atributo
        /// </summary>
        /// <param name="propriedadeDataNascimento">Nome da propriedade de data de nascimento</param>
        public SenhaValidationAttribute(string propriedadeDataNascimento) : this()
        {
            _nomePropriedadeDataNascimento = propriedadeDataNascimento;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determina se o objeto é válido
        /// </summary>
        /// <param name="value">Objeto a ser validado</param>
        /// <param name="validationContext">Contexto de validação</param>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
                return new ValidationResult("A senha deve ser informada dentro das regras de complexidade");

            string senha = value.ToString();

            // Mínimo de 06 caracteres e máximo de 08 caracteres;
            if (senha.Length < 6 || senha.Length > 8)
                return new ValidationResult("A senha deve conter entre 6 e 8 caracteres");

            // Não poderá ser igual a data de nascimento do usuário;
            if (!string.IsNullOrWhiteSpace(_nomePropriedadeDataNascimento))
            {
                object propDtNascimento = validationContext.ObjectType.GetProperty(_nomePropriedadeDataNascimento).GetValue(validationContext.ObjectInstance, null);
                if (propDtNascimento != null)
                {

                    if (DateTime.TryParse(propDtNascimento.ToString(), out DateTime dataNascimentoDt))
                    {

                        if (senha.Equals(dataNascimentoDt.ToString("ddMMyyyy")))
                            return new ValidationResult("A senha não pode ser igual a data de nascimento");
                    }
                }
            }

            // Pode conter letras e números(não é obrigatório );
            string senhaCaracteresValidos = Misc.LimparTexto(senha, "0123456789").ToLower();
            if (!senhaCaracteresValidos.ToLower().Equals(senha.ToLower()))
                return new ValidationResult("A senha deve conter apenas letras e/ou números");

            return ValidationResult.Success;

        }

        #endregion


    }
}



