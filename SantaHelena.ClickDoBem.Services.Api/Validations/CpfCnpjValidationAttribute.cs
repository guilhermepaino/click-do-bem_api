using System.ComponentModel.DataAnnotations;
using SantaHelena.ClickDoBem.Domain.Core.Tools;

namespace SantaHelena.ClickDoBem.Services.Api.Validations
{

    /// <summary>
    /// Validador de Documento (Cpf/Cnpj)
    /// </summary>
    public class CpfCnpjValidationAttribute : ValidationAttribute
    {

        /// <summary>
        /// Cria uma nova instância do atributo
        /// </summary>
        public CpfCnpjValidationAttribute() : base("Documento Cpf/Cnpj inválido")
        {
        }

        /// <summary>
        /// Determina se o objeto é válido
        /// </summary>
        /// <param name="value">Objeto a ser validado</param>
        public override bool IsValid(object value)
        {

            if (value == null)
                return false;

            return Check.VerificarDocumento(value.ToString());
        }

    }
}
