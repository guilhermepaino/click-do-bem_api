using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SantaHelena.ClickDoBem.Domain.Entities.Credenciais
{
    public class UsuarioPerfil : Core.Entities.EntityBase<UsuarioPerfil>
    {

        #region Construtores

        /// <summary>
        ///  Cria uma nova instância de UsuarioPerfil
        /// </summary>
        public UsuarioPerfil()
        {
        }

        /// <summary>
        /// Cria uma nova instância de UsuarioPerfil
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <param name="perfil">Nome do perfil</param>
        public UsuarioPerfil(Guid usuarioId, string perfil)
        {
            UsuarioId = usuarioId;
            Perfil = perfil;
        }

        #endregion

        #region Propriedades

        public Guid? UsuarioId { get; set; }

        public string Perfil { get; set; }

        #endregion

        #region  Navigation (Lazy)

        public Usuario Usuario { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {
            RuleFor(c => c.UsuarioId)
                .NotNull().WithMessage("O Id do usuário deve ser informado.");

            RuleFor(c => c.Perfil)
                .NotNull().WithMessage("O perfil deve ser informado.")
                .Must(x => x.Equals("Admin") || x.Equals("Colaborador"));
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
