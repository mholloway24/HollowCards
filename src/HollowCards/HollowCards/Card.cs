using System.Collections.Generic;

namespace HollowCards
{
    public class Card
    {
        private ICardsConfiguration _configuration { get; set; }
        private string _stringValue { get; set; }
        public object Value { get => _configuration.GetCardValue(_stringValue); }
        public string DisplayValue { get => _configuration.GetDisplayValue(this); }

        public IDictionary<string, object> ExtendedProperties { get; } = null;

        public Card(ICardsConfiguration configuration, string val)
        {
            _configuration = configuration;
            _stringValue = val;
        }

        public Card(ICardsConfiguration configuration, string val, IDictionary<string, object> extendedProps)
            : this(configuration, val)
        {
            ExtendedProperties = extendedProps;
        }
    }
}
