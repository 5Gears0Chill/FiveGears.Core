namespace FiveGears.Core.Interfaces.Configuration
{
    public interface IAppSettings
    {
        TSettings Get<TSettings>(string objString)
            where TSettings : new();
    }
}
