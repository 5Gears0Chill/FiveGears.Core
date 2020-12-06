using System;

namespace FiveGears.Core.ExceptionHandling.Exceptions
{
    public class MalformedValidationAttributeException : Exception
    {
        public MalformedValidationAttributeException(Type attributeType)
           : base($"The attribute type {attributeType} has not been defined")
        {
        }

        public MalformedValidationAttributeException(string message)
            :base(message)
        {
        }
    }
}
