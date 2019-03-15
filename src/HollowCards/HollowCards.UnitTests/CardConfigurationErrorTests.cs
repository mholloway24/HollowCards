using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
