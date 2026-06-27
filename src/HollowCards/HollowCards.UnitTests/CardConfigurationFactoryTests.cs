using HollowCards.Configurations;
using HollowCards.Utility;
using System;
using Xunit;

namespace HollowCards.UnitTests
{
    public class CardConfigurationFactoryTests
    {
        [Fact]
        public void RegisterValidConfiguration()
        {
            ICardsConfiguration config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalAceHigh);
            string testConfigName = $"{CardConfiguration.TraditionalAceHigh} - 2";

            CardConfigurationFactory.RegisterConfiguration(testConfigName, config);

            ICardsConfiguration testConfig = CardConfigurationFactory.GetConfiguration(testConfigName);

            Assert.NotNull(testConfig);
        }

        [Fact]
        public void ErrorOnNullConfigurationRegistration()
        {
            try
            {
                string testConfigName = $"{CardConfiguration.TraditionalAceHigh} - 2";
                CardConfigurationFactory.RegisterConfiguration(testConfigName, null);
            }
            catch(Exception ex)
            {
                Assert.True(ex is ArgumentException);
            }
        }

        [Fact]
        public void ErrorOnDuplicateConfigurationRegistration()
        {
            ICardsConfiguration config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);

            Assert.Throws<ArgumentException>(() =>
                CardConfigurationFactory.RegisterConfiguration(CardConfiguration.TraditionalNoJokers, config));
        }

        [Fact]
        public void ErrorOnUnknownConfigurationLookup()
        {
            Assert.Throws<ArgumentException>(() =>
                CardConfigurationFactory.GetConfiguration("NonExistentConfiguration_XYZ"));
        }

        [Fact]
        public void HasConfiguration_ReturnsTrueForRegisteredName()
        {
            Assert.True(CardConfigurationFactory.HasConfiguration(CardConfiguration.TraditionalNoJokers));
            Assert.True(CardConfigurationFactory.HasConfiguration(CardConfiguration.TraditionalAceHigh));
            Assert.True(CardConfigurationFactory.HasConfiguration(CardConfiguration.TraditionalJokers));
        }

        [Fact]
        public void HasConfiguration_ReturnsFalseForUnregisteredName()
        {
            Assert.False(CardConfigurationFactory.HasConfiguration("NonExistentConfiguration_XYZ"));
        }
    }
}
