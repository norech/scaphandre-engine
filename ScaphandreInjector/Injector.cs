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

            Initializer.PreinitStep();

            Debug.Log("Adding global Scaphandre Engine object...");
            injectorGO = new GameObject("___SCAPHANDRE_ENGINE__GO");
            injectorGO.AddComponent<SceneCleanerPreserve>();
            Object.DontDestroyOnLoad(injectorGO);
            CreateScaphandreEngineObjectTree();


            Debug.Log("Added global Scaphandre Engine object");

            Initializer.InitStep(injectorGO);
        }

        static bool isPostInitialized = false;
        public static void PostInitialize()
        {
            if (isPostInitialized) return;
            isPostInitialized = true;

            Initializer.PostinitStep();
        }

        public static void CreateScaphandreEngineObjectTree()
        {
            var workerRootObject = new GameObject("__SCAPHANDRE_WORKERS_ROOT__");
            workerRootObject.transform.parent = injectorGO.transform;

            SemlWorker.root = workerRootObject.AddComponent<SemlWorker>();

            var overlays = new GameObject("___ScaphandreOverlays__GO");
            overlays.transform.parent = injectorGO.transform;

            overlays.AddComponent<LogOverlay>();
            overlays.AddComponent<SceneOverlay>();
        }
    }
}
