using ScaphandreEngine.ModLoader;
using ScaphandreEngine.Scheduling;
using UnityEngine;

namespace ScaphandreEngine
{
    internal sealed class Initializer
    {
        public static void PreinitStep()
        {
            //
        }

        public static void InitStep(GameObject @object)
        {
            SemlLoader.Initialize();
        }

        public static void PostinitStep()
        {
        }
    }
}
