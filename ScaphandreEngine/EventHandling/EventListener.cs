using ScaphandreEngine;
using ScaphandreEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScaphandreEngine.EventHandling
{
    struct EventListener
    {
        public EventListener(Mod mod, object instance, MethodInfo methodInfo, ListenEventAttribute attribute)
        {
            this.mod = mod;
            this.methodInfo = methodInfo;
            this.instance = instance;

            var @params = methodInfo.GetParameters();

            if (@params.Length != 1)
                throw new ArgumentException("Expected only one argument in event listener");
            
            Attributes = attribute;
            ListenedEvent = @params[0].ParameterType;

            if (!typeof(Event).IsAssignableFrom(ListenedEvent))
                throw new ArgumentException(ListenedEvent.FullName + " is not an event");
        }

        private Mod mod;
        private MethodInfo methodInfo;
        private object instance;
        public ListenEventAttribute Attributes { get; private set; }
        public Type ListenedEvent { get; private set; }

        public void Call(Event @event)
        {
            try
            {
                methodInfo.FastInvoke(instance, @event);
            }
            catch (Exception ex)
            {
                mod.Logger.Error(ex);
            }
        }
    }
}
