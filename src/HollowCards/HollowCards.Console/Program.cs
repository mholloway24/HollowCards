using HollowCards.Configurations;
using HollowCards.Utility;

namespace HollowCards.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck(CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalAceHigh));
            
            while(deck.HasCards)
            {
                Card card = deck.Deal();
                System.Console.WriteLine(card.DisplayValue);
            }

            System.Console.ReadLine();
        }
    }
}
