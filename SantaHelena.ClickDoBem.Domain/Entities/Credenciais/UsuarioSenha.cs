using System;
using FluentValidation;

namespace SantaHelena.ClickDoBem.Domain.Entities.Credenciais
{

    /// <summary>
    /// Entidade de UsuarioSenha
    /// </summary>
    public class UsuarioSenha : Core.Entities.EntityBase<UsuarioSenha>
    {

        #region Construtores

        /// <summary>
        ///  Cria uma nova instância da entidade
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <param name="senha">Senha do usuário (criptografada Md5)</param>
        public UsuarioSenha(Guid usuarioId, string senha)
        {
            UsuarioId = usuarioId;
            Senha = senha;
        }

        #endregion

        #region Propriedades

        public Guid UsuarioId { get; set; }

        public string Senha { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {
            RuleFor(c => c.Senha)
                .MinimumLength(32).WithMessage("A senha deve conter 32 caracteres (hash MD5)")
                .MaximumLength(32).WithMessage("A senha deve conter 32 caracteres (hash MD5)");
        }

        #endregion

        #region Métodos Públicos

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
