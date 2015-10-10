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
            UrlOptions urlOptions = new UrlOptions {Site = SiteContext.GetSite(siteName), LanguageEmbedding = LanguageEmbedding.Always};

            return LinkManager.GetItemUrl(item, urlOptions);
        }
    }
}