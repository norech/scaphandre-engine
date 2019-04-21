using ScaphandreInjector;
using UnityEngine;

class patch_GameInput : GameInput
{

    public extern void orig_Awake();
    private void Awake()
    {
        try
        {
            Injector.Initialize();
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }

        orig_Awake();
    }

}
