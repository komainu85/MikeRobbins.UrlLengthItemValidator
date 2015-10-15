using MikeRobbins.UrlLengthItemValidator.Contracts;
using Sitecore.Configuration;

namespace MikeRobbins.UrlLengthItemValidator.Providers
{
    public class SettingsProvider : ISettingsProvider
    {
        private static string _defaultLength = "2083";

        public string GetSetting(string name)
        {
            return Settings.GetSetting(name, _defaultLength);
        }
    }
}