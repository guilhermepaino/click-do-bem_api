using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SantaHelena.ClickDoBem.Domain.Entities.Credenciais
{

    /// <summary>
    /// 
    /// </summary>
    public class Colaborador : Core.Entities.EntityIdBase<Colaborador>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de Colaborador
        /// </summary>
        public Colaborador() { }

        /// <summary>
        /// Cria uma nova instância de Colaborador
        /// </summary>
        /// <param name="cpf">Cpf do colaborador</param>
        /// <param name="ativo">Status do colaborador</param>
        public Colaborador(string cpf, bool ativo)
        {
            Cpf = cpf;
            Ativo = ativo;
        }


        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime DataAlteracao { get; set; }

        public string Cpf { get; set; }

        public bool Ativo { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O cpf deve ser informado")
                .Length(11).WithMessage("O cpf deve conter 11 dígitos");
            //TODO: Verificar como implementar aqui a validação de CPF
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
