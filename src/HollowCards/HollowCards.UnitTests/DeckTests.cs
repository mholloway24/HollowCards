using HollowCards.Configurations;
using HollowCards.Utility;
using Xunit;

namespace HollowCards.UnitTests
{
    public class DeckTests
    {
        [Theory]
        [InlineData(CardConfiguration.TraditionalNoJokers)]
        [InlineData(CardConfiguration.TraditionalAceHigh)]
        [InlineData(CardConfiguration.TraditionalJokers)]
        public void Deal_ReturnsNonNullCard(string configName)
        {
            var config = CardConfigurationFactory.GetConfiguration(configName);
            var deck = new Deck(config);

            Assert.NotNull(deck.Deal());
        }

        [Theory]
        [InlineData(CardConfiguration.TraditionalNoJokers)]
        [InlineData(CardConfiguration.TraditionalAceHigh)]
        [InlineData(CardConfiguration.TraditionalJokers)]
        public void Deal_IncrementsCurrentIndex(string configName)
        {
            var config = CardConfigurationFactory.GetConfiguration(configName);
            var deck = new Deck(config);

            Assert.Equal(0, deck.CurrentIndex);
            deck.Deal();
            Assert.Equal(1, deck.CurrentIndex);
            deck.Deal();
            Assert.Equal(2, deck.CurrentIndex);
        }

        [Theory]
        [InlineData(CardConfiguration.TraditionalNoJokers, 52)]
        [InlineData(CardConfiguration.TraditionalAceHigh, 52)]
        [InlineData(CardConfiguration.TraditionalJokers, 54)]
        public void Deal_ExhaustingAllCards_HasCardsBecomesFalse(string configName, int cardCount)
        {
            var config = CardConfigurationFactory.GetConfiguration(configName);
            var deck = new Deck(config);

            Assert.True(deck.HasCards);

            for (int i = 0; i < cardCount; i++)
                deck.Deal();

            Assert.False(deck.HasCards);
        }

        [Fact]
        public void Deal_WhenExhausted_AutoReshufflesAndReturnsCard()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var deck = new Deck(config);

            for (int i = 0; i < deck.CardsInDeck; i++)
                deck.Deal();

            Assert.False(deck.HasCards);

            Card card = deck.Deal();

            Assert.NotNull(card);
            Assert.True(deck.HasCards);
        }

        [Fact]
        public void Shuffle_ResetsCurrentIndex()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var deck = new Deck(config);

            deck.Deal();
            deck.Deal();
            deck.Deal();
            Assert.Equal(3, deck.CurrentIndex);

            deck.Shuffle();

            Assert.Equal(0, deck.CurrentIndex);
        }

        [Fact]
        public void NewGame_ResetsCurrentIndex()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var deck = new Deck(config);

            deck.Deal();
            deck.Deal();
            Assert.Equal(2, deck.CurrentIndex);

            deck.NewGame();

            Assert.Equal(0, deck.CurrentIndex);
        }

        [Fact]
        public void StringNameConstructor_CreatesValidDeck()
        {
            var deck = new Deck(CardConfiguration.TraditionalNoJokers);

            Assert.Equal(52, deck.CardsInDeck);
            Assert.True(deck.HasCards);
            Assert.Equal(0, deck.CurrentIndex);
        }

        [Fact]
        public void StartNewGameFalse_DeckIsUsableWithoutShuffle()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var deck = new Deck(config, startNewGame: false);

            Assert.Equal(52, deck.CardsInDeck);
            Assert.True(deck.HasCards);
            Assert.Equal(0, deck.CurrentIndex);
            Assert.NotNull(deck.Deal());
        }
    }
}
