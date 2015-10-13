using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MikeRobbins.UrlLengthItemValidator.Providers;
using MikeRobbins.UrlLengthItemValidator.Providers.Contracts;
using MikeRobbins.UrlLengthItemValidator.Providers.Implementation;
using Moq;
using NUnit.Framework;
using Sitecore.Data.Items;
using Sitecore.FakeDb;

namespace MikeRobbins.UrlLengthItemValidator.Tests
{
    [TestFixture]
    public class ItemUrlTests
    {
        [Test]
        public void Item_Url_Is_Correct_Length()
        {
            using (var db = new Db { new DbItem("About us") })
            {
                //Arrange
                Item aboutUs = db.GetItem("/sitecore/content/home/about us");

                Mock<ISitecoreLinkManager> sitecoreLinkManager = new Mock<ISitecoreLinkManager>();
                sitecoreLinkManager.Setup(x => x.GetItemUrl(It.IsAny<Item>(), It.IsAny<string>())).Returns("/en/about-us");

                UrlLengthCalculator urlLengthCalculator = new UrlLengthCalculator(sitecoreLinkManager.Object);

                //Act
                int length = urlLengthCalculator.GetItemUrlLength(aboutUs, "TestSite");

                //Assert
                Assert.That(length, Is.EqualTo(12));
            }
        }

        [Test]
        public void Item_Url_Is_Less_Than_Max_Length()
        {
            //Arrange
            var urlChecker = CreateSettingsMock("100");

            //Act
            bool valid = urlChecker.IsValidLength(50);

            //Assert
            Assert.That(valid, Is.EqualTo(true));
        }

        [Test]
        public void Item_Url_Is_Greater_Than_Max_Length()
        {
            //Arrange
            var urlChecker = CreateSettingsMock("100");

            //Act
            bool valid = urlChecker.IsValidLength(150);

            //Assert
            Assert.That(valid, Is.EqualTo(false));
        }

        private UrlChecker CreateSettingsMock(string settingReturnValue)
        {
            Mock<ISettingsProvider> settingsProvider = new Mock<ISettingsProvider>();
            settingsProvider.Setup(x => x.GetSetting(It.IsAny<string>())).Returns(settingReturnValue);

            UrlChecker urlChecker = new UrlChecker(settingsProvider.Object);
            return urlChecker;
        }
    }
}
