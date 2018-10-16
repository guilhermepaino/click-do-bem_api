using FluentValidation;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de TipoItem
    /// </summary>
    public class TipoItem : Core.Entities.EntityIdBase<TipoItem>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de TipoItem
        /// </summary>
        public TipoItem()
        {
            Itens = new HashSet<Item>();
        }

        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Descricao { get; set; }

        #endregion

        #region Navigation (Lazy)

        public ICollection<Item> Itens { get; set; }

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
