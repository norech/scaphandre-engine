using ScaphandreInjector.EventDelegates;

public class patch_Language : Language
{
    public extern void orig_Awake();
    private void Awake()
    {
        orig_Awake();
        main.OnLanguageChanged += LanguageDelegates.OnLanguageChanged;
    }

}
