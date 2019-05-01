using ScaphandreEngine;

namespace HelloWorld
{
    [ModInfo(
        id = "examplemods.helloworld",
        name = "Hello World Mod",
        author = "Scaphandre Contributors"
    )]
    public class Main : Mod
    {
        public override void Initialize()
        {
            Logger.Log("Hello World!");
        }
    }
}
