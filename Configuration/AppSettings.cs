using FiveGears.Core.ExceptionHandling.Exceptions;
using FiveGears.Core.Extensions;
using FiveGears.Core.Interfaces.Configuration;
using Microsoft.Extensions.Configuration;
using System;

namespace FiveGears.Core.Configuration
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TSettings Get<TSettings>(string objString) 
            where TSettings : new()
        {
            try
            {
               return _configuration.GetObjectFromConfiguration<TSettings>(objString);
            }catch(Exception ex)
            {
                throw new AppSettingsMisConfigurationException(ex.Message);
            }
        }
    }
}
