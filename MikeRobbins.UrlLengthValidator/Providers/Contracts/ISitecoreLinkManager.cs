using Sitecore.Data.Items;

namespace MikeRobbins.UrlLengthItemValidator.Providers.Contracts
{
    public interface ISitecoreLinkManager
    {
        string GetItemUrl(Item item, string siteName);
    }
}