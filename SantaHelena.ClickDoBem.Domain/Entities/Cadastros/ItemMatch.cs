using FluentValidation;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using System;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de ItemMatch
    /// </summary>
    public class ItemMatch : Core.Entities.EntityIdBase<ItemMatch>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de ItemMatch
        /// </summary>
        public ItemMatch() : base()
        {
            Data = DateTime.Now;
        }

        #endregion

        #region Propriedades

        public DateTime Data { get; set; }

        public Guid? UsuarioId { get; set; }

        public Guid? NecessidadeId { get; set; }

        public Guid? DoacaoId { get; set; }

        #endregion

        #region Navigation (Lazy)

        public Usuario Usuario { get; set; }

        public Item ItemDoacao { get; set; }

        public Item ItemNecessidade { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {

            RuleFor(c => c.UsuarioId)
                .NotNull().WithMessage("O usuário deve ser informado");

            RuleFor(c => c.NecessidadeId)
                .NotNull().WithMessage("O item de necessidade deve ser informado");

            RuleFor(c => c.DoacaoId)
                .NotNull().WithMessage("O item de doação deve ser informado");

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