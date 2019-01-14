using System;
using System.Collections.Generic;
using System.Linq;

namespace HollowCards
{
    public class Deck
    {
        private IList<Card> _cards { get; set; }
        private int _currentIndex { get; set; } = 0;

        public Deck(ICardsConfiguration configuration)
        {
            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "The configuration must be supplied to the deck");
            }

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
        }

        public void NewGame()
        {
            Shuffle();
        }
    }
}
