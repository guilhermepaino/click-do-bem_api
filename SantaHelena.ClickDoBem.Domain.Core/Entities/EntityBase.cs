using FluentValidation;
using FluentValidation.Results;
using System;

namespace SantaHelena.ClickDoBem.Domain.Core.Entities
{

    public abstract class EntityBase<T> : AbstractValidator<T> where T : EntityBase<T>
    {
        protected EntityBase()
        {
            ValidationResult = new ValidationResult();
        }

        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool EstaValido();

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return false;
        }

        public static bool operator ==(EntityBase<T> a, EntityBase<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase<T> a, EntityBase<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 900);
        }

        public override string ToString()
        {
            return $"{GetType().Name}";
        }
    }

}
