using FluentValidation;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Domain.Entities.Cadastros
{

    /// <summary>
    /// Entidade de Item
    /// </summary>
    public class Item : Core.Entities.EntityIdBase<Item>
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância de Item
        /// </summary>
        public Item()
        {
            Imagens = new HashSet<ItemImagem>();
        }

        #endregion

        #region Propriedades

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public Guid? TipoItemId { get; set; }

        public Guid? CategoriaId { get; set; }

        public Guid? UsuarioId { get; set; }

        public bool Anonimo { get; set; }

        public bool GeradoPorMatch { get; set; }

        public decimal Valor { get; set; }

        #endregion

        #region Navigation (Lazy)

        public TipoItem TipoItem { get; set; }

        public Categoria Categoria { get; set; }

        public Usuario Usuario { get; set; }

        public ICollection<ItemImagem> Imagens { get; set; }

        public ItemMatch MatchDoacao { get; set; }

        public ItemMatch MatchNecessidade { get; set; }

        #endregion

        #region Métodos Locais

        /// <summary>
        /// Valida o registro
        /// </summary>
        protected void ValidarRegistro()
        {

            RuleFor(c => c.Titulo)
                .Length(3, 50).WithMessage("O título deve conter entre 3 e 50 caracteres");

            RuleFor(c => c.Descricao)
                .MaximumLength(1000).WithMessage("A descrição deve conter no máximo 1000 caracteres");

            RuleFor(c => c.Valor)
                .GreaterThanOrEqualTo(0).WithMessage("O valor não pode ser negativo");

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
