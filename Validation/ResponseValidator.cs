using FiveGears.Core.ExceptionHandling.Exceptions;
using FiveGears.Core.Extensions.UtilityExtensions;
using FiveGears.Core.Interfaces.Validation;
using FiveGears.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FiveGears.Core.Validation
{
    public class ResponseValidator : IResponseValidator
    {
        private readonly IAttributeValidationResolver _attributeValidationResolver;
        private readonly List<Type> _attributeTypes;
        private readonly IValidationAttributeList _validationAttributeList;

        public ResponseValidator(IAttributeValidationResolver attributeValidationResolver, 
            IValidationAttributeList validationAttributeList)
        {
            _validationAttributeList = validationAttributeList;
            _attributeValidationResolver = attributeValidationResolver;
            _attributeTypes = validationAttributeList.AttributeTypes;
        }

        public ValidationModel Validate<T>(T classInstance)
        {
            if(_attributeTypes.IsNull() || _attributeTypes.Any())
            {
                throw new EmptyValidationListException();
            }
            var dict = classInstance.AsDictionary();
            foreach (var prop in dict)
            {
                var customAttr = typeof(T).GetProperty(prop.Key).GetCustomAttributes();
                if (customAttr.Any())
                {
                    var propAttr = _attributeTypes.Where(x => x == customAttr.First().GetType()).FirstOrDefault();
                    if (propAttr.IsNull()) { throw new MalformedValidationAttributeException(customAttr.First().GetType());}
                    var isDefined = Attribute.IsDefined(typeof(T).GetProperty(prop.Key), propAttr);
                    if (isDefined)
                    {
                        var validator = _attributeValidationResolver.Resolve(propAttr);
                        var validationResult = validator.Validate(prop.Value);
                        return validationResult;
                    }
                }
            }
            return ValidationModel.Success;
        }

        public ValidatorResult Map(ValidationModel model)
        {
            var messages = !model.IsValid ? model.ValidationMessage.AsStringList() : new List<string>();
            return new ValidatorResult
            {
                ErrorMessages = messages
            };
        }

        public void AddAttributes(List<Type> types)
        {
            if(!types.Where(x => x.IsSubclassOf(typeof(Attribute))).Any())
            {
                throw new MalformedValidationAttributeException($"types supplied do not extend {typeof(Attribute)}");
            }
            types.ForEach(x => _validationAttributeList.AddAttributeType(x));
        }
    }
}
