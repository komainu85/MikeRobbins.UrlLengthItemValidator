using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MikeRobbins.UrlLengthItemValidator.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Implementation;

namespace MikeRobbins.UrlLengthItemValidator.IoC
{
    public class Registry : StructureMap.Configuration.DSL.Registry
    {
        public Registry()
        {
            For<ISettingsProvider>().Use<SettingsProvider>();
            For<ISitecoreLinkManager>().Use<SitecoreLinkManager>();
            For<ISiteProvider>().Use<SiteProvider>();
            For<IUrlLengthCalculator>().Use<UrlLengthCalculator>();
            For<IUrlChecker>().Use<UrlChecker>();
        }
    }
}