using ScaphandreEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScaphandreEngine.EventHandling
{
    internal static class EventManager
    {
        static Dictionary<Type, List<EventListener>> events = new Dictionary<Type, List<EventListener>>();

        internal static void RegisterEvents(Mod mod, object instance)
        {
            var eventsHolder = instance.GetType();
            var methods = eventsHolder.GetMethods();
            foreach(var method in methods)
            {
                var attributes = method.GetCustomAttributes(true);
                foreach(var attribute in attributes)
                {
                    if(attribute.GetType() == typeof(ListenEventAttribute))
                    {
                        RegisterEvent(new EventListener(mod, instance, method, (ListenEventAttribute)attribute));
                        break;
                    }
                }
            }
        }

        private static void RegisterEvent(EventListener listener)
        {
            var listeners = events.GetOrDefault(listener.ListenedEvent, new List<EventListener>());
            listeners.Add(listener);
            events[listener.ListenedEvent] = listeners;
        }

        private static void TriggerEvent(Event e)
        {
            var listeners = events.GetOrDefault(e.GetType(), new List<EventListener>());

            foreach (var listener in listeners.OrderByDescending(l => (int)l.Attributes.priority))
            {
                listener.Call(e);
                if (e.IsPropagationStopped) break;
            }
        }
    }
}
