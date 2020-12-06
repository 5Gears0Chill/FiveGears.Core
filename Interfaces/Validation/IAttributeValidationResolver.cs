using System;

namespace FiveGears.Core.Interfaces.Validation
{
    public interface IAttributeValidationResolver
    {
        IValidator Resolve(Type attrType);
    }
}
