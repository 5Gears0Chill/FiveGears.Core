using FiveGears.Core.Constants;
using FiveGears.Core.ExceptionHandling.Exceptions;
using FiveGears.Core.ExceptionHandling.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace FiveGears.Core.ExceptionHandling.Containers
{
    public class ExceptionContainer
    {
        protected ErrorDictionary<DictionaryObject> exceptionContainer;
        public void CreateContainer()
        {
            exceptionContainer = new ErrorDictionary<DictionaryObject>();
            exceptionContainer = AddCoreErrors(exceptionContainer);
        }

        protected ErrorDictionary<DictionaryObject> AddCoreErrors(ErrorDictionary<DictionaryObject> exceptionContainer)
        {
            exceptionContainer.Add<AppSettingsMisConfigurationException>(new DictionaryObject
            {
                ResultCode = Codes.ResultCodes.AppSettings,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Title = "Failed App Settings configuration"
            });
            exceptionContainer.Add<EmptyValidationListException>(new DictionaryObject
            {
                ResultCode = Codes.ResultCodes.EmptyValidationList,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Title = "Validation functionality requires populated list"
            });
            exceptionContainer.Add<MalformedValidationAttributeException>(new DictionaryObject
            {
                ResultCode = Codes.ResultCodes.MalformedValidation,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Title = "Validation Exception: Malformed"
            });
            return exceptionContainer;
        }

        public void AddErrors(Dictionary<Type, DictionaryObject> dictionary)
        {
            foreach (KeyValuePair<Type, DictionaryObject> entry in dictionary)
            {
                exceptionContainer.Add(entry.Value, entry.Key);
            }
        } 
    }
}
