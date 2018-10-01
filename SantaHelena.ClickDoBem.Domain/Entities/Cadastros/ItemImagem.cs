using FluentValidation;
using System;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de ItemImagem
    /// </summary>
    public class ItemImagem : Core.Entities.EntityIdBase<ItemImagem>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de ItemImagem
        /// </summary>
        public ItemImagem() { }

        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public Guid? ItemId { get; set; }

        public string NomeOriginal { get; set; }

        public string Caminho { get; set; }

        #endregion

        #region Navigation (Lazy)

        public Item Item { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {

            RuleFor(c => c.NomeOriginal)
                .NotEmpty().WithMessage("O nome original do arquivo deve ser informado")
                .MaximumLength(50).WithMessage("O nome original do arquivo deve conter no máximo 50 caracteres");

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