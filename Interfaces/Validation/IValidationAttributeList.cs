using System;
using System.Collections.Generic;

namespace FiveGears.Core.Interfaces.Validation
{
    public interface IValidationAttributeList
    {
        public List<Type> AttributeTypes { get; }
        void AddAttributeType<TType>(TType type);
    }
}
