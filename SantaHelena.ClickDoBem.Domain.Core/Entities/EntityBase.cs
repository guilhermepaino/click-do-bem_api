using FluentValidation;
using FluentValidation.Results;
using System;

namespace SantaHelena.ClickDoBem.Domain.Core.Entities
{

    /// <summary>
    /// Classe abstrata de entidade
    /// </summary>
    /// <typeparam name="T">Tipo de objeto</typeparam>
    public abstract class EntityBase<T> : AbstractValidator<T> where T : EntityBase<T>
    {

        /// <summary>
        /// Cria uma nova instância da entidade
        /// </summary>
        protected EntityBase()
        {
            ValidationResult = new ValidationResult();
        }

        /// <summary>
        /// Resultado da validação
        /// </summary>
        public ValidationResult ValidationResult { get; protected set; }

        /// <summary>
        /// Verifica se o registro está válido
        /// </summary>
        public abstract bool EstaValido();

        /// <summary>
        /// Compara os dois objetos
        /// </summary>
        /// <param name="obj">Objeto para comparação</param>
        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return false;
        }

        /// <summary>
        /// Compara os dois objetos
        /// </summary>
        /// <param name="a">Objeto A</param>
        /// <param name="b">Objeto B</param>
        public static bool operator ==(EntityBase<T> a, EntityBase<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Compara os dois objetos
        /// </summary>
        /// <param name="a">Objeto A</param>
        /// <param name="b">Objeto B</param>
        public static bool operator !=(EntityBase<T> a, EntityBase<T> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Obtém o HashCode 
        /// </summary>
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 900);
        }

        /// <summary>
        /// Obtém a string do objeto
        /// </summary>
        public override string ToString()
        {
            return $"{GetType().Name}";
        }
    }

}
