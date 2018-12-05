using SantaHelena.ClickDoBem.Services.Api.Model.Request.Cadastros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaHelena.ClickDoBem.Services.Api.Validations
{

    /// <summary>
    /// Validador de upload de imagens
    /// </summary>
    public class ImagensValidationAttribute : ValidationAttribute
    {

        #region Objetos/Variáveis Locais

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do validador
        /// </summary>
        public ImagensValidationAttribute() : base("Requisição de imagens inválidas") { }

        #endregion

        #region Métodos Locais

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
                return ValidationResult.Success;

            StringBuilder sb = new StringBuilder();

            IEnumerable<SimpleImagemRequest> imagens = (IEnumerable<SimpleImagemRequest>)value;

            if (imagens.Count() > 5)
                return new ValidationResult("Limite de imagens excedido");

            int contador = 0;
            foreach (SimpleImagemRequest img in imagens)
            {

                contador++;
                if (string.IsNullOrWhiteSpace(img.NomeImagem))
                    sb.Append($"O nome da imagem {contador} deve ser informado, ");

                if (string.IsNullOrWhiteSpace(img.ImagemBase64))
                    sb.Append($"A expressão Base64 da imagem {contador} deve ser informada, ");

            }

            if (sb.Length.Equals(0))
                return ValidationResult.Success;
            else
                return new ValidationResult(sb.ToString().Substring(0, (sb.Length - 2)));

        }

        #endregion

    }

}
