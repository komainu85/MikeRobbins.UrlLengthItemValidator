using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Sites;

namespace MikeRobbins.UrlLengthItemValidator.Providers.Implementation
{
    public class SitecoreLinkManager : ISitecoreLinkManager
    {
        public string GetItemUrl(Item item, string siteName)
        {
            SiteContext site = SiteContext.GetSite(siteName);

            UrlOptions urlOptions = new UrlOptions { Site = site, LanguageEmbedding = LanguageEmbedding.Always };

            return "http://" + site.HostName + LinkManager.GetItemUrl(item, urlOptions);
        }
    }
}