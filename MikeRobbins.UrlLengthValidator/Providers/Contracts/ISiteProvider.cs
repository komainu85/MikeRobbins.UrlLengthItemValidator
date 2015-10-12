using Sitecore.Data.Items;
using Sitecore.Web;

namespace MikeRobbins.UrlLengthItemValidator.Providers.Contracts
{
    public interface ISiteProvider
    {
        SiteInfo GetSiteFromSiteItem(Item item);
    }
}