using ScaphandreEngine.EventHandling;

namespace ScaphandreEngine.Events
{
    public class LanguageChangeEvent : Event
    {
        public LanguageChangeEvent(string oldLanguage, string newLanguage)
        {
            PreviousLanguage = oldLanguage;
            Language = newLanguage;
        }

        public string PreviousLanguage { get; }
        public string Language { get; }

        public override bool IsCancellable => (PreviousLanguage != null);
    }
}
