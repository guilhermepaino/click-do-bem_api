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

        /// <summary>
        /// Cria uma nova instância do atributo
        /// </summary>
        public SenhaValidationAttribute() : base("A senha não atende a complexidade de senha.\r\n\r\nPossuir entre 6 caracteres e 8 caracteres, não poderá ser igual a data de nascimento, pode conter apenas letras e números, as letras e números não podem ser sequenciais")
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

            // Números e letras não podem ser sequenciais.
            if (PossuiSequencia(senha))
                return new ValidationResult("A senha não pode conter letras ou números na sequência (ex: AB, BC, 12, 23, etc).");

            return ValidationResult.Success;

        }

        /// <summary>
        /// Verifica se os caracteres estão em sequência
        /// </summary>
        /// <param name="expressao">Expressão a ser testada</param>
        protected bool PossuiSequencia(string expressao)
        {

            for (int p = 0; p < expressao.Length; p++)
            {

                if ((p + 1).Equals(expressao.Length))
                    break;

                char caractere1 = expressao.Substring(p, 1).ToCharArray().First();
                char caractere2 = expressao.Substring((p + 1), 1).ToCharArray().First();

                int codigo1 = caractere1;
                int codigo2 = caractere2;

                if (codigo2.Equals(codigo1 + 1) || codigo1.Equals(codigo2))
                    return true;

            }

            return false;

        }

    }
}



