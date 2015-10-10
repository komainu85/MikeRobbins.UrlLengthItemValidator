using System.Web;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Globalization;
using Sitecore.Links;

namespace MikeRobbins.UrlLengthItemValidator
{
    public class UrlLengthValidator : StandardValidator
    {
        protected override ValidatorResult Evaluate()
        {
            //Item obj = this.GetItem();


            return this.GetFailedResult(ValidatorResult.Warning);
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return this.GetFailedResult(ValidatorResult.Warning);
        }

        public override string Name {
            get { return "Total Url Length too long"; }
        }
    }
}