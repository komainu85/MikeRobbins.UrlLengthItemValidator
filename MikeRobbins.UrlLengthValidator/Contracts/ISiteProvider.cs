using Sitecore.Data.Items;
using Sitecore.Web;

namespace MikeRobbins.UrlLengthItemValidator.Contracts
{
    public interface ISiteProvider
    {
        SiteInfo GetSiteFromSiteItem(Item item);
    }
}