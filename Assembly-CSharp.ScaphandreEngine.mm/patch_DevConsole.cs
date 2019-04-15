#pragma warning disable CS0626 // orig_ method is marked external and has no attributes on it.

using ScaphandreEngine;

class patch_DevConsole : DevConsole
{
    public extern bool orig_Submit(string value);
    private bool Submit(string value)
    {
        // We call our own command resolver because our command structure is different from the Subnautica one
        if (CommandManager.ResolveCommand(this, value)) return true;

        return orig_Submit(value);
    }
}
