using System;
using Xunit;

namespace HollowCards.UnitTests
{
    public class CardConfigurationErrorTests
    {
        private ICardsConfiguration<string> configuration;

        [Fact]
        public void TestNullDeckConfiguration()
        {
            try
            {
                var deck = new Deck<string>(configuration);
            }
            catch(Exception ex)
            {
                Assert.True(ex is ArgumentException);
            }
        }

        [Fact]
        public void TestNullSuperDeckConfiguration()
        {
            try
            {
                SuperDeck deck = new SuperDeck(configuration, 2);
            }
            catch (Exception ex)
            {
                Assert.True(ex is ArgumentException);
            }
        }
    }
}
