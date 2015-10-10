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
            int itemUrlLength = 50;

            Mock<ISettingsProvider> settingsProvider = new Mock<ISettingsProvider>();
            settingsProvider.Setup(x => x.GetSetting(It.IsAny<string>())).Returns("100");

            UrlValidator urlValidator = new UrlValidator(settingsProvider.Object);

            //Act
            bool valid = urlValidator.IsValidLength(itemUrlLength);

            //Assert
            Assert.That(valid, Is.EqualTo(true));
        }

        [Test]
        public void Item_Url_Is_Greater_Than_Max_Length()
        {
            //Arrange
            int itemUrlLength = 150;

            Mock<ISettingsProvider> settingsProvider = new Mock<ISettingsProvider>();
            settingsProvider.Setup(x => x.GetSetting(It.IsAny<string>())).Returns("100");

            UrlValidator urlValidator = new UrlValidator(settingsProvider.Object);

            //Act
            bool valid = urlValidator.IsValidLength(itemUrlLength);

            //Assert
            Assert.That(valid, Is.EqualTo(false));
        }

    }
}
