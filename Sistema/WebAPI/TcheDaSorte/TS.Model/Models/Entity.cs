﻿using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace TS.Model.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }

        //USADO PARA VALIDAR USANDO DATA ANOTATIONS
        //public bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        //{
        //    results = new List<ValidationResult>();

        //    return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        //}
    }
}