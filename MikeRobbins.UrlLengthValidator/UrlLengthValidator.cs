using System.Web;
using MikeRobbins.UrlLengthItemValidator.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Implementation;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Web;

namespace MikeRobbins.UrlLengthItemValidator
{
    public class UrlLengthValidator : StandardValidator
    {
        private IUrlValidator _urlValidator;
        private IUrlLengthCalculator _urlLengthCalculator;
        private ISiteProvider _siteProvider;

        public UrlLengthValidator()
        {
            //ToDo: Remove these, locator
            _urlValidator = new UrlValidator(new SettingsProvider());
            _urlLengthCalculator = new UrlLengthCalculator(new SitecoreLinkManager());
            _siteProvider = new SiteProvider();
        }

        protected override ValidatorResult Evaluate()
        {
            Item item = this.GetItem();

            SiteInfo site = _siteProvider.GetSiteFromSiteItem(item);

            if (site != null)
            {
                int itemUrlLength = _urlLengthCalculator.GetItemUrlLength(item, site.Name);

                bool isValidLength = _urlValidator.IsValidLength(itemUrlLength);
                return this.GetFailedResult(!isValidLength ? ValidatorResult.Warning : ValidatorResult.Valid);
            }

            return this.GetFailedResult(ValidatorResult.Unknown);
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return this.GetFailedResult(ValidatorResult.Warning);
        }

        public override string Name
        {
            get { return "Total Url Length too long"; }
        }
    }
}