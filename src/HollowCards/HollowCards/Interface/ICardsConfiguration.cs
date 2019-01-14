using System.Collections;
using System.Collections.Generic;

namespace HollowCards
{
    public interface ICardsConfiguration
    {
        int NumberOfCardsInDeck { get; }
        string ConfigurationType { get; }

        IDictionary<string, string> FaceValueMapping { get; }

        IList<Card> ConfigureDeck();
        object GetCardValue(string value);
        string GetDisplayValue(Card card);        
    }
}
