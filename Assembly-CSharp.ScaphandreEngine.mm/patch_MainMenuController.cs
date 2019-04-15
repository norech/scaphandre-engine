#pragma warning disable CS0626 // orig_ method is marked external and has no attributes on it.

class patch_MainMenuController : MainMenuController
{

    public extern void orig_Update();
    private void Update()
    {
        orig_Update();
    }

}
