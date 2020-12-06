using FiveGears.Core.ExceptionHandling.Containers;
using FiveGears.Core.ExceptionHandling.Factories;
using FiveGears.Core.ExceptionHandling.Handlers;
using FiveGears.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FiveGears.Core.Middleware
{
    public class ExceptionMiddleware : ExceptionContainer
    {
        private readonly RequestDelegate _next;
     
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionsAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            try
            {
                ErrorType errorType = GenerateErrorHandler(ex);
                if (errorType.IsError())
                {
                    context.Response.StatusCode = errorType.Get().StatusCode;

                    return errorType.GenericResponse(context,
                        ErrorDetailResponseFactory.CreateKnownErrorDetails(ex.Message, errorType.Get().ResultCode, errorType.Get().Title));
                }
                return errorType.GenericResponse(context,
                       ErrorDetailResponseFactory.CreateGenericErrorDetails(ex.Message, context.Response.StatusCode, ex));
            }
            catch(Exception e)
            {
                return HandleError.WithDefaultJsonConverter(context,
                       ErrorDetailResponseFactory.CreateGenericErrorDetails(e.Message, context.Response.StatusCode, e));
            }
        }

        private ErrorType GenerateErrorHandler(Exception ex)
        {
            CreateContainer();            
            return ObjectCreator.NewInstance<ErrorType>(new object[]
               {
                    ex.GetType(),
                    exceptionContainer
               });
        }
    }
}
