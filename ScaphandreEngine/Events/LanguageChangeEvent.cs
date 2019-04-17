using ScaphandreEngine.EventHandling;

namespace ScaphandreEngine.Events
{
    public class LanguageChangeEvent : Event
    {
        internal LanguageChangeEvent(string oldLanguage, string newLanguage)
        {
            PreviousLanguage = oldLanguage;
            Language = newLanguage;
        }

        public string PreviousLanguage { get; }
        public string Language { get; }
    }
}
