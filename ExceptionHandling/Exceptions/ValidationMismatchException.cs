using FiveGears.Core.Interfaces.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveGears.Core.ExceptionHandling.Exceptions
{
    public class ValidationMismatchException : Exception
    {
        public ValidationMismatchException(Type type)
            :base($"The type {type} does not inherit from the base class of {typeof(IValidator)}")
        {
        }
    }
}
