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
            var deck = new SuperDeck<string>(CardConfiguration.TraditionalJokers, 100);
            var cardCount = 0;
            while(deck.HasCards)
            {
                var card = deck.Deal();
                cardCount++;
                System.Console.WriteLine(card.DisplayValue);
            }
            sw.Stop();

            System.Console.WriteLine($"{deck.DeckCount} decks");
            System.Console.WriteLine($"{deck.CardCount} cards");
            System.Console.WriteLine($"{cardCount} cards dealt");
            System.Console.WriteLine($"{sw.Elapsed} elapsed");
            System.Console.ReadLine();
        }
    }
}
