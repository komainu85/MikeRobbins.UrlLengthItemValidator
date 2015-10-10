using Sitecore.Data.Items;

namespace MikeRobbins.UrlLengthItemValidator.Contracts
{
    public interface IUrlLengthCalculator
    {
        int GetItemUrlLength(Item item, string siteName);
    }
}