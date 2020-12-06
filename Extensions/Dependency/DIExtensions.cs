using FiveGears.Core.Auditing;
using FiveGears.Core.Configuration;
using FiveGears.Core.ExceptionHandling.Exceptions;
using FiveGears.Core.Interfaces.Configuration;
using FiveGears.Core.Interfaces.Validation;
using FiveGears.Core.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FiveGears.Core.Extensions.Dependency
{
    public static class DIExtensions
    {
        public static IServiceCollection SetupDepencencies(this IServiceCollection services)
        {
            return services
                .RegisterAppSettings()
                .RegisterAuditing()
                .RegisterOverallValidator();
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services, List<Type> validators)
        {
            foreach (var prop in validators)
            {
                if (!(prop is IValidator)) { throw new ValidationMismatchException(prop); }
                services.AddScoped(typeof(IValidator), prop.GetType());
            }
            return services;
        }

        private static IServiceCollection RegisterAuditing(this IServiceCollection services)
        {
            services.AddScoped<IAuditManager, AuditManager>();
            return services;
        }
        private static IServiceCollection RegisterAppSettings(this IServiceCollection services)
        {
            services.AddScoped<IAppSettings, AppSettings>();
            return services;
        }

        private static IServiceCollection RegisterOverallValidator(this IServiceCollection services)
        {
            services.AddScoped<IResponseValidator, ResponseValidator>();
            services.AddScoped<IValidationAttributeList, ValidationAttributeList>();
            return services;
        }
    }
}
