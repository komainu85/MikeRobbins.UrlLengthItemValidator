﻿using System.Linq;
using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;
using Sitecore.Data.Items;
using Sitecore.Sites;
using Sitecore.Web;

namespace MikeRobbins.UrlLengthItemValidator.Providers.Implementation
{
    public class SiteProvider : ISiteProvider
    {
        public SiteInfo GetSiteFromSiteItem(Item item)
        {
            SiteInfo site =null;

            var siteInfoList = Sitecore.Configuration.Factory.GetSiteInfoList();

            foreach (SiteInfo siteInfo in siteInfoList.Where(siteInfo => item.Paths.FullPath.StartsWith(siteInfo.RootPath)))
            {
                site = siteInfo;
            }

            return site;
        }



    }
}