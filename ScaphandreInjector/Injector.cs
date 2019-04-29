using ScaphandreInjector.Overlays;
using System.IO;
using UnityEngine;
using ScaphandreEngine.ModLoader;
using ScaphandreEngine;

namespace ScaphandreInjector
{
    public static class Injector
    {
        public static GameObject injectorGO;

        static bool isInitialized = false;
        public static void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;

            LogOverlay.ListenForLogs();

            Scaphandre.Initializer.PreinitStep();

            Debug.Log("Adding global Scaphandre Engine object...");
            injectorGO = new GameObject("___SCAPHANDRE_ENGINE__GO");
            Object.DontDestroyOnLoad(injectorGO);
            CreateScaphandreEngineObjectTree();


            Debug.Log("Added global Scaphandre Engine object");

            Scaphandre.Initializer.InitStep(injectorGO);
        }

        static bool isPostInitialized = false;
        public static void PostInitialize()
        {
            if (isPostInitialized) return;
            isPostInitialized = true;

            Scaphandre.Initializer.PostinitStep();
        }

        public static void CreateScaphandreEngineObjectTree()
        {
            injectorGO.AddComponent<SemlBackgroundWorker>();

            var overlays = new GameObject("___ScaphandreOverlays__GO");
            overlays.transform.parent = injectorGO.transform;

            overlays.AddComponent<LogOverlay>();
            overlays.AddComponent<SceneOverlay>();
        }
    }
}