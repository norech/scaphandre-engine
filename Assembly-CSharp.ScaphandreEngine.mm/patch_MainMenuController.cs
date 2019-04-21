class patch_MainMenuController : MainMenuController
{

    public extern void orig_Update();
    private void Update()
    {
        orig_Update();
    }

}
