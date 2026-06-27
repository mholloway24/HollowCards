using HollowCards.Configurations;
using HollowCards.Utility;
using Xunit;

namespace HollowCards.UnitTests
{
    public class SuperDeckTests
    {
        [Fact]
        public void HasCards_TrueWhenNewlyCreated()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var superDeck = new SuperDeck(config, 2);

            Assert.True(superDeck.HasCards);
        }

        [Fact]
        public void Deal_ReturnsNonNullCard()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var superDeck = new SuperDeck(config, 2);

            Assert.NotNull(superDeck.Deal());
        }

        [Theory]
        [InlineData(CardConfiguration.TraditionalNoJokers, 2, 104)]
        [InlineData(CardConfiguration.TraditionalAceHigh, 1, 52)]
        [InlineData(CardConfiguration.TraditionalJokers, 3, 162)]
        public void Deal_ExhaustingAllCards_HasCardsBecomesFalse(string configName, int deckCount, int totalCards)
        {
            var config = CardConfigurationFactory.GetConfiguration(configName);
            var superDeck = new SuperDeck(config, deckCount);

            for (int i = 0; i < totalCards; i++)
                superDeck.Deal();

            Assert.False(superDeck.HasCards);
        }

        [Fact]
        public void Deal_WhenExhausted_AutoReshufflesAndReturnsCard()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var superDeck = new SuperDeck(config, 1);

            for (int i = 0; i < superDeck.CardCount; i++)
                superDeck.Deal();

            Assert.False(superDeck.HasCards);

            Card card = superDeck.Deal();

            Assert.NotNull(card);
            Assert.True(superDeck.HasCards);
        }

        [Fact]
        public void NewGame_ResetsExhaustedSuperDeck()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var superDeck = new SuperDeck(config, 1);

            for (int i = 0; i < superDeck.CardCount; i++)
                superDeck.Deal();

            Assert.False(superDeck.HasCards);

            superDeck.NewGame();

            Assert.True(superDeck.HasCards);
        }

        [Fact]
        public void StringNameConstructor_CreatesValidSuperDeck()
        {
            var superDeck = new SuperDeck(CardConfiguration.TraditionalNoJokers, 2);

            Assert.Equal(2, superDeck.DeckCount);
            Assert.Equal(104, superDeck.CardCount);
            Assert.True(superDeck.HasCards);
        }
    }
}
