using FluentValidation;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de categoria
    /// </summary>
    public class Categoria : Core.Entities.EntityIdBase<Categoria>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de Categoria
        /// </summary>
        public Categoria()
        {
            Itens = new HashSet<Item>();
        }

        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Descricao { get; set; }

        public int Pontuacao { get; set; }

        public bool GerenciadaRh { get; set; }

        #endregion

        #region Navigation (Lazy)

        public ICollection<Item> Itens { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {

            RuleFor(c => c.Descricao)
                .Length(3, 150).WithMessage("A descrição deve conter entre 3 e 150 caracteres");

            RuleFor(c => c.Pontuacao)
                .GreaterThan(0).WithMessage("A pontuação deve ser maior do que 0");

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
