using ScaphandreEngine;
using ScaphandreEngine.CommandHandling;

namespace SampleCommand
{
    public class SampleCommand : CommandBase
    {
        Main mod = Mod.GetMod<Main>();

        public override string GetName()
        {
            return "sample";
        }

        public override void Execute(string[] args)
        {
            mod.Logger.Log("You executed the sample command with the following arguments: " + string.Join(", ", args));
        }
    }
}
