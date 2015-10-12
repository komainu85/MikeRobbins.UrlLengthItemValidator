﻿using System.Web;
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

        readonly Container _container = new StructureMap.Container(new IoC.Registry());

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
                int itemUrlLength = _urlLengthCalculator.GetItemUrlLength(item, site.Name);

                bool isValidLength = _urlChecker.IsValidLength(itemUrlLength);
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