using FiveGears.Core.Models;
using FiveGears.Core.Validation;
using System;
using System.Collections.Generic;

namespace FiveGears.Core.Interfaces.Validation
{
    public interface IResponseValidator
    {
        ValidationModel Validate<T>(T classInstance);
        ValidatorResult Map(ValidationModel model);
        void AddAttributes(List<Type> types);
    }
}
