using FluentValidation;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de TipoMatch
    /// </summary>
    public class TipoMatch : Core.Entities.EntityIdBase<TipoMatch>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de TipoMatch
        /// </summary>
        public TipoMatch()
        {
            Matches = new HashSet<ItemMatch>();
        }

        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Descricao { get; set; }

        #endregion

        #region Navigation (Lazy)

        public ICollection<ItemMatch> Matches { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("A descrição não pode ser vazia")
                .Length(3, 150).WithMessage("A descrição deve conter entre 3 e 150 caracteres");

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
