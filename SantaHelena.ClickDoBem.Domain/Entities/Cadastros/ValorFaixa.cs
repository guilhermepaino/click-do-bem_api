using FluentValidation;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de ValorFaixa
    /// </summary>
    public class ValorFaixa : Core.Entities.EntityIdBase<ValorFaixa>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância da entidade
        /// </summary>
        public ValorFaixa()
        {
            Itens = new HashSet<Item>();
            Matches = new HashSet<ItemMatch>();
        }

        #endregion

        #region Propriedades

        public string Descricao { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal ValorFinal { get; set; }

        public bool Inativo { get; set; }

        #endregion

        #region Navigation (Lazy)

        public ICollection<Item> Itens { get; set; }

        public ICollection<ItemMatch> Matches { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {

            RuleFor(c => c.Descricao)
                .NotNull().WithMessage("A descrição deve ser informada");

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
