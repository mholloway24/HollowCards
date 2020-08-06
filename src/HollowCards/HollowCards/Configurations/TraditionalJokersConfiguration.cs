using HollowCards.Utility;
using System.Collections.Generic;
using System.Linq;

namespace HollowCards.Configurations
{
    public class TraditionalJokersConfiguration : ICardsConfiguration<string>
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
        public IList<Card<string>> ConfigureDeck()
        {
            return InitSuits();
        }

        private void InitMapping()
        {
            FaceValueMapping = FaceValueMapping ?? Constants.SuitValuesWithJoker
                .Select(cv => new { key = cv, val = GetCardValue(cv) })
                .ToDictionary(x => x.key, x => x.val);
        }

        /// <summary>
        /// Gets the <see cref="ICardsConfiguration"/> value for the string parameter
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>

        public string GetCardValue(string val)
        {
            string retVal = "-1";
            if(int.TryParse(val, out int v))
            {
                retVal = val;
            }
            else
            {
                switch (val)
                {
                    case Constants.Ace:
                        retVal = "11";
                        break;

                    case Constants.King:
                    case Constants.Queen:
                    case Constants.Jack:
                        retVal = "10";
                        break;

                    case Constants.LittleJoker:
                        retVal = "20";
                        break;

                    case Constants.BigJoker:
                        retVal = "21";
                        break;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the <see cref="ICardsConfiguration"/> display value for the <see cref="Card"/>
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public string GetDisplayValue(Card<string> card)
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
        
        private IList<Card<string>> InitSuits()
        {
            IList<Card<string>> cards = new List<Card<string>>();
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

                    cards.Add(new Card<string>(this, suitValue, props));
                }
            }

            foreach (string jokerVal in Constants.JokerValues)
            {
                IDictionary<string, object> props = new Dictionary<string, object>
                {
                    { Constants.FaceProperty, jokerVal }
                };

                cards.Add(new Card<string>(this, jokerVal, props));
            }

            if(cards.Count != this.NumberOfCardsInDeck)
            {
                throw new System.Exception("Invalid configuration: card count does not match the expected number of cards in this configuration");
            }

            return cards;
        }
    }
}
