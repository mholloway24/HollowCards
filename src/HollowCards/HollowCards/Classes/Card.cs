using System.Collections.Generic;

namespace HollowCards
{
    /// <summary>
    /// One of a collection contained in a <see cref="Deck"/> and is initialized through <seealso cref="ICardsConfiguration"/>
    /// </summary>
    public class Card<T>
    {
        private ICardsConfiguration<T> _configuration { get; set; }
        private string _stringValue { get; set; }
        public T Value { get => _configuration.GetCardValue(_stringValue); }
        public string DisplayValue { get => _configuration.GetDisplayValue(this); }

        /// <summary>
        /// Configured by the <code>ICardConfiguration</code> to provide extra information about each card
        /// </summary>
        public IDictionary<string, object> ExtendedProperties { get; } = null;

        /// <summary>
        /// Initialize a Card object using the <see cref="ICardsConfiguration"/> and string value
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="val"></param>
        public Card(ICardsConfiguration<T> configuration, string val)
        {
            _configuration = configuration;
            _stringValue = val;
        }

        /// <summary>
        /// Initialize a Card object using the <see cref="ICardsConfiguration"/>, string value, and extended properties
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="val"></param>
        /// <param name="extendedProps"></param>
        public Card(ICardsConfiguration<T> configuration, string val, IDictionary<string, object> extendedProps)
            : this(configuration, val)
        {
            ExtendedProperties = extendedProps;
        }
    }
}
