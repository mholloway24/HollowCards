using HollowCards.Configurations;
using HollowCards.Utility;

namespace HollowCards.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SuperDeck deck = new SuperDeck(CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalAceHigh), 4);
            
            while(deck.HasCards)
            {
                Card card = deck.Deal();
                System.Console.WriteLine(card.DisplayValue);
            }

            System.Console.WriteLine(deck.DeckCount);
            System.Console.ReadLine();
        }
    }
}
