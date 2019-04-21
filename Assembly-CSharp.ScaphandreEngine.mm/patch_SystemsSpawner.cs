using ScaphandreInjector;
using UnityEngine;

class patch_SystemsSpawner : SystemsSpawner
{
    public extern void orig_Awake();
    private void Awake()
    {
        orig_Awake();

        try
        {
            Injector.PostInitialize();
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
