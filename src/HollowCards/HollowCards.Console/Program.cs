using HollowCards.Configurations;
using System;

namespace HollowCards.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck(CardConfigurationFactory.GetConfiguration(CardConfiguration.TraditionalNoJokers));

            deck.NewGame();

            while(deck.HasCards)
            {
                Card card = deck.Deal();
                System.Console.WriteLine(card.DisplayValue);
            }

            System.Console.ReadLine();
        }
    }
}
