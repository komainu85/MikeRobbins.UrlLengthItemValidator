using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Globalization;

namespace MikeRobbins.UrlLengthValidator
{
    public class UrlLengthValidator : StandardValidator
    {
        protected override ValidatorResult Evaluate()
        {
            Item obj = this.GetItem();
            if (obj == null || !obj.Paths.IsContentItem)
                return ValidatorResult.Valid;
            string name = obj.Name;
            string str1 = HttpUtility.UrlEncode(name);
            if (name != str1)
            {
                this.Text = this.GetText(Translate.Text("The item name contains characters that will be encoded when used in a link."));
                return this.GetFailedResult(ValidatorResult.Warning);
            }
            string displayName = obj.DisplayName;
            string str2 = HttpUtility.UrlEncode(displayName);
            if (!(displayName != str2))
                return ValidatorResult.Valid;
            this.Text = this.GetText(Translate.Text("The display item name contains characters that will be encoded when they are used in a link."));
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