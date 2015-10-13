using MikeRobbins.UrlLengthItemValidator.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;

namespace MikeRobbins.UrlLengthItemValidator
{
    public class UrlChecker : IUrlChecker
    {
        private const string _maxUrlLength = "UrlValidator.MaxUrlLength";

        private readonly ISettingsProvider _settingsProvider;

        public UrlChecker(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public bool IsValidLength(int itemUrlLength)
        {
            return itemUrlLength < MaxLengthAllowed();
        }

        public int MaxLengthAllowed()
        {
            string setting = _settingsProvider.GetSetting(_maxUrlLength);

            return ParseSetting(setting);
        }

        private int ParseSetting(string setting)
        {
            int maxLength = 0;

            int.TryParse(setting, out maxLength);
            return maxLength;
        }
    }
}