using System.Web;
using MikeRobbins.UrlLengthItemValidator.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Implementation;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using StructureMap;

namespace MikeRobbins.UrlLengthItemValidator
{
    public class UrlLengthValidator : StandardValidator
    {
        private readonly IUrlChecker _urlChecker;
        private readonly IUrlLengthCalculator _urlLengthCalculator;
        private readonly ISiteProvider _siteProvider;

        private readonly Container _container = new StructureMap.Container(new IoC.Registry());

        private int currentLength = 0;

        public UrlLengthValidator()
        {
            _urlChecker = _container.GetInstance<IUrlChecker>();
            _urlLengthCalculator = _container.GetInstance<IUrlLengthCalculator>();
            _siteProvider = _container.GetInstance<ISiteProvider>();
        }

        protected override ValidatorResult Evaluate()
        {
            Item item = this.GetItem();

            SiteInfo site = _siteProvider.GetSiteFromSiteItem(item);

            if (site != null)
            {
                currentLength = _urlLengthCalculator.GetItemUrlLength(item, site.Name);

                bool isValidLength = _urlChecker.IsValidLength(currentLength);

                return Validate(isValidLength);
            }

            return this.GetFailedResult(ValidatorResult.Unknown);
        }

        private ValidatorResult Validate(bool isValidLength)
        {
            if (!isValidLength)
            {
                this.Text = string.Format("The full URL of this item is too long. URL length is {0}, Max is {1} ", currentLength, _urlChecker.MaxLengthAllowed());
                return this.GetFailedResult(ValidatorResult.Warning);
            }
            else
            {
                return ValidatorResult.Valid;
            }
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return this.GetFailedResult(ValidatorResult.Warning);
        }

        public override string Name
        {
            get
            {
                return "URL too long";
            }
        }

    }
}