using System;
using System.Collections.Generic;
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
        public Usuario() : this(null, null)
        {
        }

        /// <summary>
        /// Cria uma nova instância de usuário
        /// </summary>
        /// <param name="nome">Nome do usuário</param>
        /// <param name="cpfCnpj">Cpf/Cnpj do usuário</param>
        public Usuario(string nome, string cpfCnpj)
        {
            Nome = nome;
            CpfCnpj = cpfCnpj;
            UsuarioPerfil = new HashSet<UsuarioPerfil>();
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
        /// Número do documento
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }

        #endregion

        #region Navigation (Lazy)

        public UsuarioLogin UsuarioLogin { get; set; }

        public UsuarioDados UsuarioDados { get; set; }

        public ICollection<UsuarioPerfil> UsuarioPerfil { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome deve ser informado")
                .Length(3, 150).WithMessage("O nome deve conter entre 3 e 150 caracteres");
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
