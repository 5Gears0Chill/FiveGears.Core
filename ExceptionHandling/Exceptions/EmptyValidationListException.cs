using System;

namespace FiveGears.Core.ExceptionHandling.Exceptions
{
    public class EmptyValidationListException : Exception
    {
        public EmptyValidationListException()
            :base($"validation list is empty. Add validation attributes by calling Add Attributes.")
        {
        }
    }
}
