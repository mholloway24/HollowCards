using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollowCards.Configurations
{
    public static class CardConfigurationFactory
    {
        public static ICardsConfiguration GetConfiguration(string name)
        {
            if(!CardConfiguration.HasConfiguration(name))
            {
                throw new ArgumentException($"Card Configuration '{name}' cannot be found");
            }
            return CardConfiguration.Configurations[name];
        }
    }
}
