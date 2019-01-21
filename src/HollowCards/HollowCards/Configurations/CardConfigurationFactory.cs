using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollowCards.Configurations
{
    /// <summary>
    /// Centralizes the <see cref="ICardsConfiguration"/> registration
    /// </summary>
    public static class CardConfigurationFactory
    {
        /// <summary>
        /// List of registered card configurations
        /// </summary>
        public static IDictionary<string, ICardsConfiguration> Configurations { get; internal set; }
        
        static CardConfigurationFactory()
        {
            Configurations = new Dictionary<string, ICardsConfiguration>
            {
                { CardConfiguration.TraditionalNoJokers, new TraditionalNoJokersConfiguration() }
            };
        }

        /// <summary>
        /// Registers a custom card configuration with the <see cref="CardConfigurationFactory">CardConfigurationFactory</see>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configuration"></param>
        public static void RegisterConfiguration(string name, ICardsConfiguration configuration)
        {
            if (Configurations.ContainsKey(name))
            {
                throw new ArgumentException($"Card Configuration '{name}' already exists");
            }

            Configurations.Add(name, configuration);
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
            return Configurations[name];
        }
    }
}
