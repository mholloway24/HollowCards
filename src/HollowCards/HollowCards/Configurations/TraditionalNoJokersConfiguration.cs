using HollowCards.Utility;
using System.Collections.Generic;
using System.Linq;

namespace HollowCards.Configurations
{
    public class TraditionalNoJokersConfiguration : ICardsConfiguration<string>
    {
        public int NumberOfCardsInDeck { get => 52; }
        public string ConfigurationType { get => CardConfiguration.TraditionalNoJokers; }
        
        public IDictionary<string, string> FaceValueMapping { get; private set; }

        private readonly string DisplayFormat = "{0} of {1}";

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
            FaceValueMapping = FaceValueMapping ?? Constants.SuitValues
                .Select(sv => new { key = sv, val = GetCardValue(sv) })
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

            if (int.TryParse(val, out int v))
            {
                retVal = val;
            }
            else
            {
                switch (val)
                {
                    case Constants.Ace:
                        retVal = "1";
                        break;

                    case Constants.King:
                    case Constants.Queen:
                    case Constants.Jack:
                        retVal = "10";
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
            return string.Format(DisplayFormat, card.ExtendedProperties[Constants.FaceProperty], card.ExtendedProperties[Constants.SuitProperty]);
        }

        private IList<Card<string>> InitSuits()
        {
            IList<Card<string>> cards = new List<Card<string>>();
            InitMapping();

            foreach (string suit in Constants.Suits)
            {

                foreach (KeyValuePair<string, string> kvp in FaceValueMapping)
                {
                    IDictionary<string, object> props = new Dictionary<string, object>
                    {
                        { Constants.SuitProperty, suit },
                        { Constants.FaceProperty, kvp.Key }
                    };

                    cards.Add(new Card<string>(this, kvp.Key, props));
                }
            }

            return cards;
        }
    }
}
