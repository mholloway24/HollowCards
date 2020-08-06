using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
