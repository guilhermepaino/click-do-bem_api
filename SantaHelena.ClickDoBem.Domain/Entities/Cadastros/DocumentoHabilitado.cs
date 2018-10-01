using FluentValidation;
using System;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{
    public class DocumentoHabilitado : Core.Entities.EntityIdBase<DocumentoHabilitado>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de DocumentosHabilitados
        /// </summary>
        public DocumentoHabilitado() { }

        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string CpfCnpj { get; set; }

        public bool Ativo { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {
            RuleFor(c => c.CpfCnpj)
                .NotEmpty().WithMessage("O documento (Cpf/Cnpj) deve ser informado");
        }

        #endregion

        #region Métodos Públicos / Overrides

        /// <summary>
        /// Verifica se o registro está válido
        /// </summary>
        public override bool EstaValido()
        {
            ValidarRegistro();
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        #endregion

    }
}