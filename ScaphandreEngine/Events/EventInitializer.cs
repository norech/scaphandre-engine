using ScaphandreEngine.EventHandling;
using UnityEngine;

namespace ScaphandreEngine.Events
{
    internal static class EventInitializer
    {
        public static void Initialize()
        {
            Debug.Log("Registering events...");

            string previousLanguage = null;
            Language.main.OnLanguageChanged += delegate ()
            {
                var language = Language.main.GetCurrentLanguage();
                EventManager.TriggerEvent(new LanguageChangeEvent(previousLanguage, language));
                previousLanguage = language;
            };

            Debug.Log("Registered events.");
        }
    }
}
