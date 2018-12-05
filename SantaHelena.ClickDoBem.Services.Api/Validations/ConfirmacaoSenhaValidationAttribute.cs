using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using SantaHelena.ClickDoBem.Domain.Core.Tools;

namespace SantaHelena.ClickDoBem.Services.Api.Validations
{

    /// <summary>
    /// Validador de complexidade de confirmação de senha
    /// </summary>
    public class ConfirmacaoSenhaValidationAttribute : ValidationAttribute
    {

        #region Objeto/Variáveis Locais

        /// <summary>
        /// Nome da propriedade de data de nova senha
        /// </summary>
        protected readonly string _nomePropriedadeNovaSenha;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do atributo
        /// </summary>
        /// <param name="propriedadeNovaSenha">Nome da propriedade de nova senha</param>
        public ConfirmacaoSenhaValidationAttribute(string propriedadeNovaSenha) : base()
        {
            _nomePropriedadeNovaSenha = propriedadeNovaSenha;
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

            // Comparando a confirmação desenha coma nova senha
            if (!string.IsNullOrWhiteSpace(_nomePropriedadeNovaSenha))
            {


                PropertyInfo propNovaSenha = validationContext.ObjectType.GetProperty(_nomePropriedadeNovaSenha);
                if (propNovaSenha == null)
                    throw new ArgumentException($"A propriedade {_nomePropriedadeNovaSenha} não foi localizada");

                object valorNovaSenha = propNovaSenha.GetValue(validationContext.ObjectInstance, null);
                if (value.ToString() != valorNovaSenha.ToString())
                    return new ValidationResult("A senha e a confirmação de senha não conferem entre si.");

            }

            return ValidationResult.Success;

        }

        #endregion


    }
}



