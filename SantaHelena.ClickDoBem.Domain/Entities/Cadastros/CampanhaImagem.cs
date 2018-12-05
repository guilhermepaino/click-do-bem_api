using FluentValidation;
using System;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de CampanhaImagem
    /// </summary>
    public class CampanhaImagem : Core.Entities.EntityIdBase<CampanhaImagem>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de CampanhaImagem
        /// </summary>
        public CampanhaImagem() { }

        #endregion

        #region Propriedades

        public Guid? CampanhaId { get; set; }

        public string Caminho { get; set; }

        #endregion

        #region Navigation (Lazy)

        public Campanha Campanha { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {

            RuleFor(c => c.Caminho)
                .NotEmpty().WithMessage("O caminho deve ser informado")
                .MaximumLength(2000).WithMessage("O caminho informado ultrapassou o limite de 2000 caracteres");

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