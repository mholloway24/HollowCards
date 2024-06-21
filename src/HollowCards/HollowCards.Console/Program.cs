using HollowCards.Configurations;
using HollowCards.Utility;
using System.Diagnostics;

namespace HollowCards.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            SuperDeck deck = new SuperDeck(CardConfiguration.TraditionalJokers, 10000);
            
            while(deck.HasCards)
            {
                Card card = deck.Deal();
                System.Console.WriteLine(card.DisplayValue);
            }
            sw.Stop();

            System.Console.WriteLine($"{deck.DeckCount} decks");
            System.Console.WriteLine($"{deck.CardCount} cards");
            System.Console.WriteLine($"{sw.Elapsed} elapsed");
            System.Console.ReadLine();
        }
    }
}
