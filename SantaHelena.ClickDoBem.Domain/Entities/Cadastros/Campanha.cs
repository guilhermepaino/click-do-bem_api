using FluentValidation;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de Campanha
    /// </summary>
    public class Campanha : Core.Entities.EntityIdBase<Campanha>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de Campanha
        /// </summary>
        public Campanha()
        {
            Itens = new HashSet<Item>();
        }

        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataInicial { get; set; }

        public DateTime? DataFinal { get; set; }

        public int Prioridade { get; set; }

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
                .NotNull().WithMessage("A descrição deve ser informada");

            RuleFor(c => c.DataInicial)
                .NotNull().WithMessage("A data inicial deve ser informada");

            RuleFor(c => c.DataFinal)
                .NotNull().WithMessage("A data final deve ser informada");

            RuleFor(c => c.Prioridade)
                .InclusiveBetween(0, 3).WithMessage("A prioridade deve ser: 0=baixa / 1=Normal / 2=Alta / 3=Altíssima");

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
