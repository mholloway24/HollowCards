using HollowCards.Configurations;
using HollowCards.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HollowCards.UnitTests
{
    public class CardConfigurationTests
    {
        private ICardsConfiguration<string> configuration;

        [Theory]
        [InlineData(CardConfiguration.TraditionalAceHigh, "TraditionalAceHigh", 52)]
        [InlineData(CardConfiguration.TraditionalJokers, "TraditionalJokers", 54)]
        [InlineData(CardConfiguration.TraditionalNoJokers, "TraditionalNoJokers", 52)]
        public void EnsureDeckCardCount(string configurationName, string expectedConfigurationName, int expectedCardCount)
        {
            configuration = CardConfigurationFactory.GetConfiguration<string>(configurationName);

            var deck = new Deck<string>(configuration);
            Assert.Equal(expectedConfigurationName, configuration.ConfigurationType);
            Assert.Equal(expectedCardCount, deck.CardsInDeck);
            Assert.True(deck.HasCards);
        }

        [Theory]
        [InlineData("TraditionalAceHigh", 2, 104)]
        [InlineData("TraditionalJokers", 3, 162)]
        [InlineData("TraditionalNoJokers", 4, 208)]
        public void EnsureSuperDeckCardCount(string configurationName, int deckCount, int expectedCardCount)
        {
            configuration = CardConfigurationFactory.GetConfiguration<string>(configurationName);

            var deck = new SuperDeck<string>(configuration, deckCount);
            Assert.Equal(expectedCardCount, deck.CardCount);
        }

        [Theory]
        [InlineData("TraditionalAceHigh", new string[] { }, true)]
        [InlineData("TraditionalJokers", new string[] { Constants.BigJoker, Constants.LittleJoker }, true)]
        [InlineData("TraditionalNoJokers", new string[] { }, true)]
        public void TestCardSuits(string configurationName, string[] noSuitValues, bool expectedHasExtendedProperties)
        {
            configuration = CardConfigurationFactory.GetConfiguration<string>(configurationName);
            IList<string> noSuitValuesList = noSuitValues.ToList();
            var deck = new Deck<string>(configuration);
            Card<string> card = null;

            while (deck.HasCards)
            {
                card = deck.Deal();
                Assert.NotNull(card);
                Assert.Equal(expectedHasExtendedProperties, card.ExtendedProperties != null);

                if (expectedHasExtendedProperties)
                {
                    Assert.Equal(!card.ExtendedProperties.ContainsKey(Constants.SuitProperty),
                        noSuitValues.Select(v => configuration.FaceValueMapping[v]).Contains(card.Value));
                }
            }
        }
    }
}
