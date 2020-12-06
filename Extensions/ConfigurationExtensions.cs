using Microsoft.Extensions.Configuration;
using System;

namespace FiveGears.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetObjectFromConfiguration<T>(this IConfiguration configuration, string section)
            where T : new()
        {
            var result = new T();
            foreach (var propInfo in typeof(T).GetProperties())
            {
                var propertyType = propInfo.PropertyType;
                if (propInfo?.CanWrite ?? false)
                {
                    var value = Convert.ChangeType(configuration.GetValue<string>
                        ($"{section}:{propInfo.Name}"), propertyType);
                    propInfo.SetValue(result, value, null);
                }
            }
            return result;
        }
    }
}
