using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;
using Sitecore.Configuration;

namespace MikeRobbins.UrlLengthItemValidator.Providers.Implementation
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