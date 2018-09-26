using System;
using FluentValidation;

namespace SantaHelena.ClickDoBem.Domain.Entities
{
    public class Usuario : Core.Entities.EntityIdBase<Usuario>
    {

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime DataAlteracao { get; set; }

        public string Nome { get; set; }

        #endregion

        #region Métodos Locais

        protected void ValidarRegistro()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome deve ser informado")
                .MinimumLength(3).WithMessage("O nome deve conter no mínimo 3 caracteres")
                .MaximumLength(150).WithMessage("O nome deve conter no máximo 150 caracteres");
        }

        #endregion

        #region Métodos Públicos / Overrides

        public override bool EstaValido()
        {
            ValidarRegistro();
            ValidationResult = Validate(this);


            return ValidationResult.IsValid;
        }

        #endregion

    }
}
