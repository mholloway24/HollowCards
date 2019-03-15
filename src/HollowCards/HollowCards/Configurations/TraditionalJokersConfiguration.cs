using HollowCards.Utility;
using System.Collections.Generic;

namespace HollowCards.Configurations
{
    public class TraditionalJokersConfiguration : ICardsConfiguration
    {
        public int NumberOfCardsInDeck { get => 54; }
        public string ConfigurationType { get => CardConfiguration.TraditionalJokers; }

        public IDictionary<string, string> FaceValueMapping { get; private set; }

        private readonly string DefaultFormat = "{0} of {1}";
        private readonly string JokerFormat = "{0} Joker";


        /// <summary>
        /// Configures the <see cref="Deck"/> object's cards according to this <see cref="ICardsConfiguration"/>
        /// </summary>
        /// <returns></returns>
        public IList<Card> ConfigureDeck()
        {
            return InitSuits();
        }

        private void InitMapping()
        {
            FaceValueMapping = new Dictionary<string, string>();
            Constants.SuitValuesWithJoker.ForEach(val =>
            {
                if (!FaceValueMapping.ContainsKey(val))
                {
                    if (int.TryParse(val, out int v))
                    {
                        FaceValueMapping.Add(val, val);
                    }
                    else
                    {
                        switch (val)
                        {
                            case Constants.Ace:
                                FaceValueMapping.Add(val, "11");
                                break;

                            case Constants.King:
                            case Constants.Queen:
                            case Constants.Jack:
                                FaceValueMapping.Add(val, "10");
                                break;

                            case Constants.LittleJoker:
                                FaceValueMapping.Add(val, "20");
                                break;

                            case Constants.BigJoker:
                                FaceValueMapping.Add(val, "21");
                                break;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Gets the <see cref="ICardsConfiguration"/> value for the string parameter
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public object GetCardValue(string val)
        {
            return FaceValueMapping[val];
        }

        /// <summary>
        /// Gets the <see cref="ICardsConfiguration"/> display value for the <see cref="Card"/>
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public string GetDisplayValue(Card card)
        {
            switch (card.ExtendedProperties[Constants.FaceProperty])
            {
                case Constants.LittleJoker:
                    return string.Format(JokerFormat, "Little");

                case Constants.BigJoker:
                    return string.Format(JokerFormat, "Big");

                default:
                    return string.Format(DefaultFormat, card.ExtendedProperties[Constants.FaceProperty], card.ExtendedProperties[Constants.SuitProperty]);
            }
        }

        private IList<Card> InitSuits()
        {
            IList<Card> cards = new List<Card>();
            InitMapping();

            foreach (string suit in Constants.Suits)
            {

                foreach (string suitValue in Constants.SuitValues)
                {
                    IDictionary<string, object> props = new Dictionary<string, object>
                    {
                        { Constants.SuitProperty, suit },
                        { Constants.FaceProperty, suitValue }
                    };

                    cards.Add(new Card(this, suitValue, props));
                }
            }

            foreach (string jokerVal in Constants.JokerValues)
            {
                IDictionary<string, object> props = new Dictionary<string, object>
                {
                    { Constants.FaceProperty, jokerVal }
                };

                cards.Add(new Card(this, jokerVal, props));
            }

            if(cards.Count != this.NumberOfCardsInDeck)
            {
                throw new System.Exception("Invalid configuration: card count does not match the expected number of cards in this configuration");
            }

            return cards;
        }
    }
}
