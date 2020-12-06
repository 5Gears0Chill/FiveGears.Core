using FiveGears.Core.Models;
using System;

namespace FiveGears.Core.Interfaces.Validation
{
    public interface IValidator
    {
        public bool IsApplicable(Type attrType);
        public ValidationModel Validate(object value);
    }
}
