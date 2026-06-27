using HollowCards.Configurations;
using HollowCards.Utility;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HollowCards.UnitTests
{
    public class CardTests
    {
        // --- GetCardValue: Ace mapping differs by configuration ---

        [Theory]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.Ace, "1")]
        [InlineData(CardConfiguration.TraditionalAceHigh, Constants.Ace, "11")]
        [InlineData(CardConfiguration.TraditionalJokers, Constants.Ace, "11")]
        public void GetCardValue_Ace_MapsToExpectedValue(string configName, string face, string expected)
        {
            var config = CardConfigurationFactory.GetConfiguration(configName);
            config.ConfigureDeck();

            Assert.Equal(expected, config.GetCardValue(face));
        }

        // --- GetCardValue: face cards (J, Q, K) are always 10 ---

        [Theory]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.Jack)]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.Queen)]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.King)]
        [InlineData(CardConfiguration.TraditionalAceHigh, Constants.Jack)]
        [InlineData(CardConfiguration.TraditionalAceHigh, Constants.Queen)]
        [InlineData(CardConfiguration.TraditionalAceHigh, Constants.King)]
        [InlineData(CardConfiguration.TraditionalJokers, Constants.Jack)]
        [InlineData(CardConfiguration.TraditionalJokers, Constants.Queen)]
        [InlineData(CardConfiguration.TraditionalJokers, Constants.King)]
        public void GetCardValue_FaceCards_EqualTen(string configName, string face)
        {
            var config = CardConfigurationFactory.GetConfiguration(configName);
            config.ConfigureDeck();

            Assert.Equal("10", config.GetCardValue(face));
        }

        // --- GetCardValue: number cards map to their own string value ---

        [Theory]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.Two)]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.Seven)]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.Ten)]
        [InlineData(CardConfiguration.TraditionalAceHigh, Constants.Three)]
        [InlineData(CardConfiguration.TraditionalAceHigh, Constants.Nine)]
        [InlineData(CardConfiguration.TraditionalJokers, Constants.Five)]
        [InlineData(CardConfiguration.TraditionalJokers, Constants.Eight)]
        public void GetCardValue_NumberCards_EqualTheirFaceString(string configName, string face)
        {
            var config = CardConfigurationFactory.GetConfiguration(configName);
            config.ConfigureDeck();

            Assert.Equal(face, config.GetCardValue(face));
        }

        // --- GetCardValue: joker-specific values ---

        [Fact]
        public void GetCardValue_LittleJoker_EqualsTwenty()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalJokers);
            config.ConfigureDeck();

            Assert.Equal("20", config.GetCardValue(Constants.LittleJoker));
        }

        [Fact]
        public void GetCardValue_BigJoker_EqualsTwentyOne()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalJokers);
            config.ConfigureDeck();

            Assert.Equal("21", config.GetCardValue(Constants.BigJoker));
        }

        // --- Card.Value delegates to configuration ---

        [Fact]
        public void CardValue_AceInNoJokersConfig_ReturnsOne()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var cards = config.ConfigureDeck();
            var ace = cards.First(c => (string)c.ExtendedProperties[Constants.FaceProperty] == Constants.Ace);

            Assert.Equal("1", (string)ace.Value);
        }

        [Fact]
        public void CardValue_AceInAceHighConfig_ReturnsEleven()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalAceHigh);
            var cards = config.ConfigureDeck();
            var ace = cards.First(c => (string)c.ExtendedProperties[Constants.FaceProperty] == Constants.Ace);

            Assert.Equal("11", (string)ace.Value);
        }

        // --- GetDisplayValue: normal cards use "{Face} of {Suit}" format ---

        [Theory]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.Ace, Constants.Clubs, "A of Clubs")]
        [InlineData(CardConfiguration.TraditionalNoJokers, Constants.King, Constants.Hearts, "K of Hearts")]
        [InlineData(CardConfiguration.TraditionalAceHigh, Constants.Ace, Constants.Spades, "A of Spades")]
        [InlineData(CardConfiguration.TraditionalAceHigh, Constants.Two, Constants.Diamonds, "2 of Diamonds")]
        [InlineData(CardConfiguration.TraditionalJokers, Constants.Queen, Constants.Clubs, "Q of Clubs")]
        [InlineData(CardConfiguration.TraditionalJokers, Constants.Ten, Constants.Hearts, "10 of Hearts")]
        public void DisplayValue_NormalCard_ShowsFaceOfSuit(string configName, string face, string suit, string expected)
        {
            var config = CardConfigurationFactory.GetConfiguration(configName);
            var cards = config.ConfigureDeck();
            var card = cards.First(c =>
                (string)c.ExtendedProperties[Constants.FaceProperty] == face &&
                c.ExtendedProperties.ContainsKey(Constants.SuitProperty) &&
                (string)c.ExtendedProperties[Constants.SuitProperty] == suit);

            Assert.Equal(expected, card.DisplayValue);
        }

        // --- GetDisplayValue: jokers use "{Size} Joker" format ---

        [Fact]
        public void DisplayValue_BigJoker_ShowsBigJoker()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalJokers);
            var cards = config.ConfigureDeck();
            var bigJoker = cards.First(c => (string)c.ExtendedProperties[Constants.FaceProperty] == Constants.BigJoker);

            Assert.Equal("Big Joker", bigJoker.DisplayValue);
        }

        [Fact]
        public void DisplayValue_LittleJoker_ShowsLittleJoker()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalJokers);
            var cards = config.ConfigureDeck();
            var littleJoker = cards.First(c => (string)c.ExtendedProperties[Constants.FaceProperty] == Constants.LittleJoker);

            Assert.Equal("Little Joker", littleJoker.DisplayValue);
        }

        // --- ExtendedProperties content ---

        [Fact]
        public void ExtendedProperties_AllNormalCards_HaveFaceAndSuit()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var deck = new Deck(config);

            while (deck.HasCards)
            {
                Card card = deck.Deal();
                Assert.NotNull(card.ExtendedProperties);
                Assert.True(card.ExtendedProperties.ContainsKey(Constants.FaceProperty));
                Assert.True(card.ExtendedProperties.ContainsKey(Constants.SuitProperty));
                Assert.NotNull(card.ExtendedProperties[Constants.FaceProperty]);
                Assert.NotNull(card.ExtendedProperties[Constants.SuitProperty]);
            }
        }

        [Fact]
        public void ExtendedProperties_JokerCards_HaveNoSuit()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalJokers);
            var cards = config.ConfigureDeck();
            var jokers = cards.Where(c =>
                (string)c.ExtendedProperties[Constants.FaceProperty] == Constants.BigJoker ||
                (string)c.ExtendedProperties[Constants.FaceProperty] == Constants.LittleJoker).ToList();

            Assert.Equal(2, jokers.Count);
            foreach (var joker in jokers)
                Assert.False(joker.ExtendedProperties.ContainsKey(Constants.SuitProperty));
        }

        [Fact]
        public void ExtendedProperties_CorrectValues_WhenSetOnConstruction()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var card = new Card(config, Constants.Ace, new Dictionary<string, object>
            {
                { Constants.FaceProperty, Constants.Ace },
                { Constants.SuitProperty, Constants.Clubs }
            });

            Assert.Equal(Constants.Ace, card.ExtendedProperties[Constants.FaceProperty]);
            Assert.Equal(Constants.Clubs, card.ExtendedProperties[Constants.SuitProperty]);
        }

        // --- Card constructors ---

        [Fact]
        public void CardConstructor_WithoutExtendedProperties_ExtendedPropertiesIsNull()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            config.ConfigureDeck(); // initialize FaceValueMapping before calling card.Value
            var card = new Card(config, Constants.Ace);

            Assert.Null(card.ExtendedProperties);
            Assert.Equal("1", (string)card.Value);
        }

        [Fact]
        public void CardConstructor_WithExtendedProperties_PropertiesAreStored()
        {
            var config = CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers);
            var props = new Dictionary<string, object>
            {
                { Constants.FaceProperty, Constants.King },
                { Constants.SuitProperty, Constants.Diamonds }
            };
            var card = new Card(config, Constants.King, props);

            Assert.NotNull(card.ExtendedProperties);
            Assert.Equal(2, card.ExtendedProperties.Count);
            Assert.Equal(Constants.King, card.ExtendedProperties[Constants.FaceProperty]);
            Assert.Equal(Constants.Diamonds, card.ExtendedProperties[Constants.SuitProperty]);
        }
    }
}
