using MikeRobbins.UrlLengthItemValidator.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;

namespace MikeRobbins.UrlLengthItemValidator
{
    public class UrlValidator : IUrlValidator
    {
        private const string _maxUrlLength = "MaxUrlLength";


        private ISettingsProvider _settingsProvider;

        public UrlValidator(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public bool IsValidLength(int itemUrlLength)
        {
            string setting = _settingsProvider.GetSetting(_maxUrlLength);

            var maxLength = ParseSetting(setting);

            return itemUrlLength < maxLength;
        }

        private int ParseSetting(string setting)
        {
            int maxLength = 0;

            int.TryParse(setting, out maxLength);
            return maxLength;
        }
    }
}