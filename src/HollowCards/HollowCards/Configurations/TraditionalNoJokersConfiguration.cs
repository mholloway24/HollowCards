using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HollowCards.Configurations
{
    public class TraditionalNoJokersConfiguration : ICardsConfiguration
    {
        public int NumberOfCardsInDeck { get => 1; }
        public string ConfigurationType { get => "TraditionalNoJokers"; }

        public IDictionary<string, string> FaceValueMapping { get; private set; }

        private readonly List<string> Suits = new List<string>() { "Clubs", "Spades", "Hearts", "Diamonds" };
        private List<string> Values = new List<string>() { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        private readonly string DisplayFormat = "{0} of {1}";

        private readonly string SuitProperty = "Suit";
        private readonly string FaceProperty = "Face";

        public IList<Card> ConfigureDeck()
        {
            return InitSuits();
        }

        private void InitMapping()
        {
            FaceValueMapping = new Dictionary<string, string>();
            Values.ForEach(val =>
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
                            FaceValueMapping.Add(val, "1");
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

        public object GetCardValue(string val)
        {
            return FaceValueMapping[val];
        }

        public string GetDisplayValue(Card card)
        {
            return string.Format(DisplayFormat, card.ExtendedProperties[FaceProperty], card.ExtendedProperties[SuitProperty]);
        }


        private IList<Card> InitSuits()
        {
            IList<Card> cards = new List<Card>();
            InitMapping();

            foreach (string suit in Suits)
            {

                foreach (KeyValuePair<string, string> kvp in FaceValueMapping)
                {
                    IDictionary<string, object> props = new Dictionary<string, object>();
                    props.Add(SuitProperty, suit);
                    props.Add(FaceProperty, kvp.Key);

                    cards.Add(new Card(this, kvp.Key, props));
                }
            }

            return cards;
        }
    }
}
