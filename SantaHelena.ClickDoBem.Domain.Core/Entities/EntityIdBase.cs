using System;
using System.Collections.Generic;
using System.Text;

namespace SantaHelena.ClickDoBem.Domain.Core.Entities
{
    public abstract class EntityIdBase<T> : EntityBase<T> where T : EntityIdBase<T>
    {

        #region Construtores

        public EntityIdBase() : base()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Propriedades

        public Guid Id { get; protected set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityIdBase<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 900) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} - Id = {Id}";
        }

        #endregion

    }
}
