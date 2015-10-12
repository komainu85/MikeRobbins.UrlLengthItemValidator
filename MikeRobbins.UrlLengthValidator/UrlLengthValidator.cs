using System.Web;
using MikeRobbins.UrlLengthItemValidator.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Implementation;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Globalization;
using Sitecore.Links;

namespace MikeRobbins.UrlLengthItemValidator
{
    public class UrlLengthValidator : StandardValidator
    {
        private IUrlValidator _urlValidator;
        private IUrlLengthCalculator _urlLengthCalculator;

        public UrlLengthValidator()
        {
            _urlValidator = new UrlValidator(new SettingsProvider());
            _urlLengthCalculator = new UrlLengthCalculator(new SitecoreLinkManager());
        }

        protected override ValidatorResult Evaluate()
        {
            Item item = this.GetItem();

            int itemUrlLength = _urlLengthCalculator.GetItemUrlLength(item, "SiteName");

            bool isValidLength = _urlValidator.IsValidLength(itemUrlLength);

            return this.GetFailedResult(!isValidLength ? ValidatorResult.Warning : ValidatorResult.Valid);
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