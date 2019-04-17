using ScaphandreEngine.EventHandling;

namespace ScaphandreEngine.Events
{
    public class LanguageChangeEvent : Event
    {
        internal LanguageChangeEvent(string newLanguage)
        {
            Language = newLanguage;
        }

        public string Language { get; }
    }
}
