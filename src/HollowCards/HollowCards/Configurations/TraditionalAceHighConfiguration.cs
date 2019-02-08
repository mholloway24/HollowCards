using HollowCards.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HollowCards.Configurations
{
    public class TraditionalAceHighConfiguration : ICardsConfiguration
    {
        public int NumberOfCardsInDeck { get => 52; }
        public string ConfigurationType { get => CardConfiguration.TraditionalAceHigh; }
        
        public IDictionary<string, string> FaceValueMapping { get; private set; }

        private readonly string DisplayFormat = "{0} of {1}";


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
            Constants.SuitValues.ForEach(val =>
            {
                int v;

                if (int.TryParse(val, out v))
                {
                    FaceValueMapping.Add(val, val);
                }
                else
                {
                    switch (val)
                    {
                        case "A":
                            FaceValueMapping.Add(val, "11");
                            break;

                        case "K":
                        case "Q":
                        case "J":
                            FaceValueMapping.Add(val, "10");
                            break;
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
            return string.Format(DisplayFormat, card.ExtendedProperties[Constants.FaceProperty], card.ExtendedProperties[Constants.SuitProperty]);
        }

        private IList<Card> InitSuits()
        {
            IList<Card> cards = new List<Card>();
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

                    cards.Add(new Card(this, kvp.Key, props));
                }
            }

            return cards;
        }
    }
}
