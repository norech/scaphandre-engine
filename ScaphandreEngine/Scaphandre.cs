using ScaphandreEngine.EventHandling;
using ScaphandreEngine.Events;
using ScaphandreEngine.ModLoader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine
{
    public class Scaphandre
    {
        private static string _informationalVersion = null;
        private static string _version = null;

        public static string InformationalVersion
        {
            get {
                if (_informationalVersion == null)
                {
                    _informationalVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
                }

                return _informationalVersion;
            }
        }

        public static string Version
        {
            get
            {
                if (_version == null)
                {
                    _version = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
                }

                return _version;
            }
        }

        public static bool SupportsGameVersion => SharedAssemblyInfo.IsSupportedVersion(Subnautica.Version);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed class Initializer
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
                Debug.Log("Registering events...");

                Language.main.OnLanguageChanged += () => 
                    EventManager.TriggerEvent(new LanguageChangeEvent(Language.main.GetCurrentLanguage()));

                Debug.Log("Registered events.");
            }
        }
    }
}
