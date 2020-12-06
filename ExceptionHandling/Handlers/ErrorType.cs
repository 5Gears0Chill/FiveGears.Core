using FiveGears.Core.ExceptionHandling.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FiveGears.Core.ExceptionHandling.Handlers
{
    public class ErrorType
    {
        private readonly ErrorDictionary<DictionaryObject> _dictionary;
        private Type _exceptionType;

        public ErrorType(Type exceptionType, ErrorDictionary<DictionaryObject> errors)
        {
            _dictionary = errors;
            _exceptionType = exceptionType;
        }

        public bool IsError()
        {
            return _dictionary.ContainsType(this._exceptionType);
        }

        public DictionaryObject Get()
        {
            return _dictionary.Get(this._exceptionType);
        }

        public Task GenericResponse<K>(HttpContext context, K model)
        {
            return InvokeResponseWithCustomJsonConverter(context, model);
        }

        private Task InvokeResponseWithCustomJsonConverter<K>(HttpContext context, K model)
        {
            return HandleError.WithDefaultJsonConverter(context, model);
        }
    }
}
