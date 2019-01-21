using System.Collections.Generic;

namespace HollowCards
{
    /// <summary>
    /// Provides all necessary information to configure a <see cref="Deck"/> of <see cref="Card"/> objects
    /// </summary>
    public interface ICardsConfiguration
    {
        /// <summary>
        /// The number of <see cref="Card"/> objects in the Deck
        /// </summary>
        int NumberOfCardsInDeck { get; }

        /// <summary>
        /// The string representation of this <see cref="ICardsConfiguration"/> type
        /// </summary>
        string ConfigurationType { get; }

        /// <summary>
        /// The face value to card value mapping
        /// </summary>
        IDictionary<string, string> FaceValueMapping { get; }

        /// <summary>
        /// Configure the cards used in the <see cref="Deck"/> object
        /// </summary>
        /// <returns></returns>
        IList<Card> ConfigureDeck();

        /// <summary>
        /// Provide this <see cref="ICardsConfiguration"/> value based on a string value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        object GetCardValue(string value);

        /// <summary>
        /// Provided the display value of this <see cref="Card"/>
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        string GetDisplayValue(Card card);
    }
}
