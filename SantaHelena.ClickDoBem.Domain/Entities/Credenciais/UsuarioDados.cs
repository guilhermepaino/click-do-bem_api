using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaHelena.ClickDoBem.Domain.Entities.Credenciais
{

    /// <summary>
    /// Entidade de UsuarioDados
    /// </summary>
    public class UsuarioDados : Core.Entities.EntityIdBase<UsuarioDados>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de Dados de Usuário
        /// </summary>
        public UsuarioDados() { }

        /// <summary>
        /// Cria uma nova instância de Dados de Usuário
        /// </summary>
        /// <param name="usuarioId"></param>
        public UsuarioDados(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }

        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public Guid? UsuarioId { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        public string CEP { get; set; }

        public string TelefoneCelular { get; set; }

        public string TelefoneFixo { get; set; }

        public string Email { get; set; }

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

            RuleFor(c => c.UsuarioId)
                .NotNull().WithMessage("O id do usuário deve ser informado");

            RuleFor(c => c.Logradouro)
                .Length(3, 100).WithMessage("O logradouro deve conter entre 3 e 100 caracteres");

            RuleFor(c => c.Numero)
                .Length(1, 30).WithMessage("O número deve conter entre 1 e 30 caracteres");

            RuleFor(c => c.Complemento)
                .MaximumLength(50).WithMessage("O complemento deve conter no máximo 50 caracteres");

            RuleFor(c => c.Bairro)
                .Length(3, 100).WithMessage("O bairro deve conter entre 3 e 80 caracteres");

            RuleFor(c => c.Cidade)
                .Length(3, 100).WithMessage("A cidade deve conter entre 3 e 100 caracteres");

            RuleFor(c => c.UF)
                .Length(2).WithMessage("A Uf (estado) deve conter 2 caracteres")
                .Must((u, c) => ValidaEstado(u)).WithMessage("Estado inválido");

            RuleFor(c => c.CEP)
                .Length(8).WithMessage("O Cep deve conter 8 caracteres");

            RuleFor(c => c.TelefoneFixo)
                .MaximumLength(20).WithMessage("O Telefone fixo deve conter no máximo 20 posições");

            RuleFor(c => c.TelefoneCelular)
                .MaximumLength(20).WithMessage("O Telefone celular deve conter no máximo 20 posições");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Email inválido");

        }

        private bool ValidaEstado(UsuarioDados u)
        {
            List<string> ufs = (new string[] { "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA", "MG", "MS", "MT", "PA", "PB", "PE", "PI", "PR", "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO" }).ToList();
            return (!string.IsNullOrWhiteSpace(ufs.Find(x => x.Equals(u))));
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
