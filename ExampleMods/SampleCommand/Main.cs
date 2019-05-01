using ScaphandreEngine;

namespace SampleCommand
{
    [ModInfo(
        id = "examplemods.samplecommand",
        name = "Sample Command Mod",
        author = "Scaphandre Contributors"
    )]
    public class Main : Mod
    {
        public override void Initialize()
        {
            var registry = GameRegistry.ForMod(this);

            registry.RegisterCommand(new SampleCommand());
        }
    }
}
