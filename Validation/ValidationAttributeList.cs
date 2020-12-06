using FiveGears.Core.Interfaces.Validation;
using System;
using System.Collections.Generic;

namespace FiveGears.Core.Validation
{
    public class ValidationAttributeList : IValidationAttributeList
    {
        public List<Type> AttributeTypes { get; private set; }

        public ValidationAttributeList()
        {
            AttributeTypes = new List<Type>();
        }

        public void AddAttributeType<TType>(TType type)
        {
            AttributeTypes.Add(type.GetType());
        }


    }
}
