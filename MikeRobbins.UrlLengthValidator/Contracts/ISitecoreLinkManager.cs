using Sitecore.Data.Items;

namespace MikeRobbins.UrlLengthItemValidator.Contracts
{
    public interface ISitecoreLinkManager
    {
        string GetItemUrl(Item item, string siteName);
    }
}