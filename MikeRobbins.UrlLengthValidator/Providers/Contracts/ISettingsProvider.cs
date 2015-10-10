namespace MikeRobbins.UrlLengthItemValidator.Providers.Contracts
{
    public interface ISettingsProvider
    {
        string GetSetting(string name);
    }
}