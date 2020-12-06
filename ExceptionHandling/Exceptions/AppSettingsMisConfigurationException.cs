using System;

namespace FiveGears.Core.ExceptionHandling.Exceptions
{
    public class AppSettingsMisConfigurationException : Exception
    {
        public AppSettingsMisConfigurationException(string message)
            :base(message: message)
        {
        }
    }
}
