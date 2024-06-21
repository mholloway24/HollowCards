using HollowCards.Configurations;
using System;
using System.Collections.Generic;

namespace HollowCards.Utility
{
    /// <summary>
    /// Centralizes the <see cref="ICardsConfiguration"/> registration
    /// </summary>
    public static class CardConfigurationFactory
    {
        /// <summary>
        /// List of registered card configurations
        /// </summary>
        public static IDictionary<string, Type> Configurations { get; internal set; }
        
        static CardConfigurationFactory()
        {
            Configurations = new Dictionary<string, Type>
            {
                { CardConfiguration.TraditionalNoJokers, typeof(TraditionalNoJokersConfiguration) },
                { CardConfiguration.TraditionalAceHigh, typeof(TraditionalAceHighConfiguration) },
                { CardConfiguration.TraditionalJokers, typeof(TraditionalJokersConfiguration) }
            };
        }

        /// <summary>
        /// Registers a custom card configuration with the <see cref="CardConfigurationFactory">CardConfigurationFactory</see>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configuration"></param>
        public static void RegisterConfiguration(string name, ICardsConfiguration configuration)
        {
            if (HasConfiguration(name))
            {
                throw new ArgumentException($"Card Configuration '{name}' already exists");
            }

            if(configuration is null)
            {
                throw new ArgumentException($"Card Configuration '{name}' cannot be null");
            }

            Configurations.Add(name, configuration.GetType());
        }

        /// <summary>
        /// Is an <see cref="ICardsConfiguration"/> identifier already registered?
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasConfiguration(string name)
        {
            return Configurations.ContainsKey(name);
        }

        /// <summary>
        /// Retrieve the registered <see cref="ICardsConfiguration"/> for this identifier
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ICardsConfiguration GetConfiguration(string name)
        {
            if(!HasConfiguration(name))
            {
                throw new ArgumentException($"Card Configuration '{name}' cannot be found");
            }
            return (ICardsConfiguration)Activator.CreateInstance(Configurations[name]);
        }

        public static ICardsConfiguration<T> GetConfiguration<T>(string name)
        {
            if(!HasConfiguration(name))
            {
                throw new ArgumentException($"Card Configuration '{name}' cannot be found");
            }
            return (ICardsConfiguration<T>)Activator.CreateInstance(Configurations[name]);
        }
    }
}
