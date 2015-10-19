using System.Web;
using MikeRobbins.UrlLengthItemValidator.Contracts;
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

        public UrlLengthValidator()
        {
            _urlChecker = _container.GetInstance<IUrlChecker>();
            _urlLengthCalculator = _container.GetInstance<IUrlLengthCalculator>();
            _siteProvider = _container.GetInstance<ISiteProvider>();
        }

        protected override ValidatorResult Evaluate()
        {
            Item item = this.GetItem();

            if (item != null)
            {
                SiteInfo site = _siteProvider.GetSiteFromSiteItem(item);

                if (site != null)
                {
                    int urlLength = _urlLengthCalculator.GetItemUrlLength(item, site.Name);

                    bool isValidLength = _urlChecker.IsValidLength(urlLength);

                    return Validate(isValidLength, urlLength);
                }
            }

            return this.GetFailedResult(ValidatorResult.Unknown);
        }

        private ValidatorResult Validate(bool isValidLength, int urlLength)
        {
            if (!isValidLength)
            {
                this.Text = string.Format("The full URL of this item is too long. URL length is {0}, Max is {1} ", urlLength, _urlChecker.MaxLengthAllowed());
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