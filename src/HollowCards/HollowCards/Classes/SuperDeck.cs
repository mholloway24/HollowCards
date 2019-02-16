using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HollowCards
{
    /// <summary>
    /// This class supports multiple <see cref="Deck"/> objects within a single deck of <seealso cref="Card"/> objects
    /// </summary>
    public class SuperDeck 
    {
        /// <summary>
        /// The number of <see cref="Deck"/> objects contained in this <see cref="SuperDeck"/>
        /// </summary>
        public int DeckCount { get; }

        private IList<Deck> _decks;

        /// <summary>
        /// Configures the <see cref="SuperDeck"/> object
        /// </summary>
        /// <param name="configuration">The <see cref="ICardsConfiguration"/> configuration for all decks in this object</param>
        /// <param name="numberOfDecks">Number of <see cref="Deck"/> objects</param>
        public SuperDeck(ICardsConfiguration configuration, int numberOfDecks = 1)
        {
            _decks = new List<Deck>();
            Parallel.ForEach(Enumerable.Range(0, numberOfDecks), index =>
            {
                Deck d = new Deck(configuration);
                lock (this)
                {
                    _decks.Add(d);
                }
            });
            DeckCount = numberOfDecks;
        }

        public bool HasCards => _decks.Any(d => d.HasCards);        

        public Card Deal()
        {
            if(!HasCards)
            {
                Shuffle();
            }

            Deck nextDeck = _decks.OrderBy(d => d.CurrentIndex).First();
            return nextDeck.Deal();
        }

        public void NewGame()
        {
            Shuffle();
        }

        public void Shuffle()
        {
            Parallel.ForEach(_decks, _deck =>
            {
                _deck.Shuffle();
            });
        }
    }
}
