using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace HollowCards
{
    public class Deck
    {
        private IList<Card> _cards { get; set; }
        private int _currentIndex { get; set; } = 0;
        private RNGCryptoServiceProvider _randomProvider { get; }

        public Deck(ICardsConfiguration configuration)
        {
            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "The configuration must be supplied to the deck");
            }

            _randomProvider = new RNGCryptoServiceProvider();
            _cards = configuration.ConfigureDeck();
        }

        public bool HasCards => _currentIndex < _cards.Count();

        public Card Deal()
        {
            if (_currentIndex >= _cards.Count())
            {
                Shuffle();                
            }

            return _cards[_currentIndex++];
        }

        public void Shuffle()
        {
            _currentIndex = 0;

            for(int i = 0; i < _cards.Count; i++)
            {
                byte swap = RollDice((byte)(_cards.Count - 1));
                SwapCards(i, swap);
            }
        }

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

        private byte RollDice(byte numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberSides));

            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                _randomProvider.GetBytes(randomNumber);
            }
            while (!IsFairRoll(randomNumber[0], numberSides));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % numberSides) + 1);
        }

        private static bool IsFairRoll(byte roll, byte numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
        }
    }
}
