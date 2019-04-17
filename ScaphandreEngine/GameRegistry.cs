using ScaphandreEngine.CommandHandling;
using ScaphandreEngine.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine
{
    public class GameRegistry
    {
        static Dictionary<Mod, GameRegistry> modRegistries = new Dictionary<Mod, GameRegistry>();

        public static Dictionary<Mod, GameRegistry> GetModRegistries()
        {
            return new Dictionary<Mod, GameRegistry>(modRegistries);
        }

        public static GameRegistry ForMod(Mod mod)
        {
            if(!modRegistries.TryGetValue(mod, out GameRegistry registry))
            {
                registry = new GameRegistry(mod);
                modRegistries.Add(mod, registry);
            }

            return registry;
        }

        Mod mod;

        internal GameRegistry(Mod mod)
        {
            this.mod = mod;
        }

        public void RegisterCommand(Command command)
        {
            CommandManager.RegisterCommand(mod, command);
        }

        public void RegisterEvents(object eventsHolder)
        {
            EventManager.RegisterEvents(mod, eventsHolder);
        }
    }
}
