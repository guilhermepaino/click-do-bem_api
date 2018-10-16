using System;
using FluentValidation;

namespace SantaHelena.ClickDoBem.Domain.Entities.Credenciais
{

    /// <summary>
    /// Entidade de Login de Usuário
    /// </summary>
    public class UsuarioLogin : Core.Entities.EntityBase<UsuarioLogin>
    {

        #region Construtores

        public UsuarioLogin()
        { }

        /// <summary>
        ///  Cria uma nova instância da entidade
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <param name="senha">Senha do usuário (criptografada Md5)</param>
        public UsuarioLogin(Guid usuarioId, string senha)
        {
            UsuarioId = usuarioId;
            Senha = senha;
        }

        #endregion

        #region Propriedades

        public Guid UsuarioId { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        #endregion

        #region Navigation (Lazy)

        public Usuario Usuario { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {

            RuleFor(c => c.Login)
                .NotNull().WithMessage("O Login deve ser informado");

            RuleFor(c => c.Senha)
                .Length(32).WithMessage("A senha deve conter 32 caracteres (hash MD5)");
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
