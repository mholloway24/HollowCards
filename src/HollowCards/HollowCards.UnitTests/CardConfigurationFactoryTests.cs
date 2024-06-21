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
    }
}
