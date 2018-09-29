using System;
using FluentValidation;

namespace SantaHelena.ClickDoBem.Domain.Entities.Credenciais
{

    /// <summary>
    /// Entidade de Usuário
    /// </summary>
    public class Usuario : Core.Entities.EntityIdBase<Usuario>
    {

        #region Construtores

        /// <summary>
        ///  Cria uma nova instância de usuário
        /// </summary>
        public Usuario()
        {
        }

        /// <summary>
        /// Número do documento
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Cria uma nova instância de usuário
        /// </summary>
        /// <param name="nome">Nome do usuário</param>
        public Usuario(string nome)
        {
            Nome = nome;
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Data de inclusão do registro
        /// </summary>
        public DateTime DataInclusao { get; set; }

        /// <summary>
        /// Data da última alteração do registro
        /// </summary>
        public DateTime? DataAlteracao { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }

        #endregion

        #region Navigation (Lazy)

        public UsuarioLogin UsuarioLogin { get; set; }

        public UsuarioDados UsuarioDados { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome deve ser informado")
                .MinimumLength(3).WithMessage("O nome deve conter no mínimo 3 caracteres")
                .MaximumLength(150).WithMessage("O nome deve conter no máximo 150 caracteres");
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
