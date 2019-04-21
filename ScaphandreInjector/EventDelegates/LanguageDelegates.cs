using ScaphandreEngine.EventHandling;
using ScaphandreEngine.Events;

namespace ScaphandreInjector.EventDelegates
{
    internal static class LanguageDelegates
    {
        private static string previousLanguage = null;
        private static bool isLanguageChangeCancelled = false;

        public static void OnLanguageChanged()
        {
            // We don't trigger when cancelled
            if (isLanguageChangeCancelled)
            {
                isLanguageChangeCancelled = false;
                return;
            }

            var language = Language.main.GetCurrentLanguage();
            if (EventManager.TriggerEvent(new LanguageChangeEvent(previousLanguage, language)).IsCancelled)
            {
                isLanguageChangeCancelled = true;
                Language.main.SetCurrentLanguage(previousLanguage);
            }
            else
            {
                previousLanguage = language;
            }
        }
    }
}
