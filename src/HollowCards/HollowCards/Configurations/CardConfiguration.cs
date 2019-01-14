using System;
using System.Collections.Generic;

namespace HollowCards.Configurations
{
    public static class CardConfiguration
    {
        public static IDictionary<string, ICardsConfiguration> Configurations { get; internal set; }

        /// <summary>
        ///  Traditional Card Configuration with No Jokers
        /// </summary>
        public static string TraditionalNoJokers = "TraditionalNoJokers";

        static CardConfiguration()
        {
            Configurations = new Dictionary<string, ICardsConfiguration>
            {
                { TraditionalNoJokers, new TraditionalNoJokersConfiguration() }
            };
        }

        public static void RegisterConfiguration(string name, ICardsConfiguration configuration)
        {
            if (Configurations.ContainsKey(name))
            {
                throw new ArgumentException($"Card Configuration '{name}' already exists");
            }

            Configurations.Add(name, configuration);
        }

        public static bool HasConfiguration(string name)
        {
            return Configurations.ContainsKey(name);
        }
    }
}
