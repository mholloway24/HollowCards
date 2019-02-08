using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace HollowCards
{
    /// <summary>
    /// Containes a collection of <see cref="Card"/> objects defined by a <seealso cref="ICardsConfiguration"/> object
    /// </summary>
    public class Deck
    {
        private IList<Card> _cards { get; set; }
        private int _currentIndex { get; set; } = 0;
        private RNGCryptoServiceProvider _randomProvider { get; }

        /// <summary>
        /// Initialize a deck of cards using this <see cref="ICardsConfiguration"/>
        /// </summary>
        /// <param name="configuration"></param>
        private Deck(ICardsConfiguration configuration)
        {
            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "The configuration must be supplied to the deck");
            }

            _randomProvider = new RNGCryptoServiceProvider();
            _cards = configuration.ConfigureDeck();
        }

        public Deck(ICardsConfiguration configuration, bool startNewGame = true) : this(configuration)
        {
            if(startNewGame)
            {
                NewGame();
            }
        }

        /// <summary>
        /// Are there cards left in the deck?
        /// </summary>
        public bool HasCards => _currentIndex < _cards.Count();

        /// <summary>
        /// Retrieve the top card on the deck
        /// </summary>
        /// <returns></returns>
        public Card Deal()
        {
            if (_currentIndex >= _cards.Count())
            {
                Shuffle();                
            }

            return _cards[_currentIndex++];
        }

        /// <summary>
        /// Shuffle the deck and reset the card count
        /// </summary>
        public void Shuffle()
        {
            _currentIndex = 0;

            for(int i = 0; i < _cards.Count; i++)
            {
                byte swap = PickCard((byte)(_cards.Count - 1));
                SwapCards(i, swap);
            }
        }

        /// <summary>
        /// Start a new game by shuffling the deck
        /// </summary>
        public void NewGame()
        {
            Shuffle();
        }

        private void SwapCards(int first, int second)
        {
            Card temp = _cards[first];
            _cards[first] = _cards[second];
            _cards[second] = temp;
        }

        private byte PickCard(byte numberCards)
        {
            if (numberCards <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberCards));

            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                _randomProvider.GetBytes(randomNumber);
            }
            while (!IsFairChoice(randomNumber[0], numberCards));
            // Return the random number mod the number
            // of cards.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % numberCards) + 1);
        }

        private static bool IsFairChoice(byte choice, byte numCards)
        {
            // There are MaxValue / numCards full sets of numbers that can come up
            // in a single byte.
            int fullSetsOfValues = Byte.MaxValue / numCards;

            // If the choice is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return choice < numCards * fullSetsOfValues;
        }
    }
}
