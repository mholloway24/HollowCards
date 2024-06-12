using System;
using Xunit;

namespace HollowCards.UnitTests
{
    public class CardConfigurationErrorTests
    {
        private ICardsConfiguration configuration;

        [Fact]
        public void TestNullDeckConfiguration()
        {
            try
            {
                Deck deck = new Deck(configuration);
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
