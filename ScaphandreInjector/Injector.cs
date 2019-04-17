using ScaphandreInjector.Overlays;
using System.IO;
using UnityEngine;
using ScaphandreEngine.ModLoader;

namespace ScaphandreInjector
{
    public static class Injector
    {
        public static GameObject injectorGO;

        public static void Initialize()
        {
            LogOverlay.ListenForLogs();

            Debug.Log("Adding global Scaphandre Engine object...");
            injectorGO = new GameObject("___SCAPHANDRE_ENGINE__GO");
            Object.DontDestroyOnLoad(injectorGO);
            CreateScaphandreEngineObjectTree();
            Debug.Log("Added global Scaphandre Engine object");

            var modsFolder = Path.GetFullPath("./Mods");
            Debug.Log("SEML mods folder: " + modsFolder);
            
            Debug.Log("Loading mods...");
            SemlLoader.instance.LoadModsFromFolder(modsFolder);
            Debug.Log("Loaded " + SemlLoader.instance.LoadedModsCount + " mods");
        }

        public static void CreateScaphandreEngineObjectTree()
        {
            var overlays = new GameObject("___ScaphandreOverlays__GO");
            overlays.transform.parent = injectorGO.transform;

            overlays.AddComponent<LogOverlay>();
            overlays.AddComponent<SceneOverlay>();
        }
    }
}