using MikeRobbins.UrlLengthItemValidator.Contracts;
using Sitecore.Data.Items;

namespace MikeRobbins.UrlLengthItemValidator
{
    public class UrlLengthCalculator : IUrlLengthCalculator
    {
        private readonly ISitecoreLinkManager _sitecoreLinkManager;

        public UrlLengthCalculator(ISitecoreLinkManager sitecoreLinkManager)
        {
            _sitecoreLinkManager = sitecoreLinkManager;
        }

        public int GetItemUrlLength(Item item, string siteName)
        {
            string url = _sitecoreLinkManager.GetItemUrl(item, siteName);

            return url.Length;
        }
    }
}