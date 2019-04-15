#pragma warning disable CS0626 // orig_ method is marked external and has no attributes on it.
using ScaphandreInjector;

public class patch_GameInput : GameInput
{

    public extern void orig_Awake();
    private void Awake()
    {
        Injector.Initialize();
        orig_Awake();
    }

}
