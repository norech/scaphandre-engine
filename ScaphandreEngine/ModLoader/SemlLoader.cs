
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ScaphandreEngine.ModLoader
{
    public class SemlLoader
    {
        public static SemlLoader instance = new SemlLoader();

        private static Dictionary<string, SemlInfo> mods = new Dictionary<string, SemlInfo>();

        internal SemlLoader() {
        }

        internal static void Initialize()
        {
            var modsFolder = Path.GetFullPath("./Mods");
            Debug.Log("SEML mods folder: " + modsFolder);

            Debug.Log("Loading mods...");
            instance.LoadModsFromFolder(modsFolder);
            Debug.Log("Loaded " + instance.LoadedModsCount + " mods");
        }

        internal static SemlInfo GetSemlInfo(Assembly assembly)
        {
            foreach (var semlInfo in mods)
            {
                if (semlInfo.Value.assembly == assembly)
                {
                    return semlInfo.Value;
                }
            }
            return null;
        }

        internal static SemlInfo GetSemlInfo(Type modType)
        {
            foreach (var semlInfo in mods)
            {
                if (semlInfo.Value.mod.GetType() == modType)
                {
                    return semlInfo.Value;
                }
            }
            return null;
        }

        internal static SemlInfo GetSemlInfo(Mod mod)
        {
            foreach (var semlInfo in mods)
            {
                if (semlInfo.Value.mod == mod)
                {
                    return semlInfo.Value;
                }
            }
            return null;
        }

        internal static SemlInfo GetSemlInfo(string mod)
        {
            return mods.GetOrDefault(mod, null);
        }

        public List<string> LoadedModsList => new List<string>(mods.Keys);

        public int LoadedModsCount => mods.Count;

        public void LoadModsFromFolder(string modsFolder)
        {
            foreach(var file in Directory.GetFiles(modsFolder))
            {
                if (Path.GetExtension(file).ToLower() != ".dll") continue;

                var name = Path.GetFileNameWithoutExtension(file);
                var modAssembly = Assembly.LoadFrom(file);

                var entryPointClasses = modAssembly.GetTypes()
                    .Where(searchedType => searchedType.IsClass && searchedType.IsSubclassOf(typeof(Mod)));

                var entryPointCount = entryPointClasses.Count();
                if (entryPointCount != 1)
                {
                    Debug.LogError("Zero or multiple mod entry points were found for '" + name + "' assembly. Requiring only one. Skipping mod.");
                    continue;
                }

                var mod = (Mod)Activator.CreateInstance(entryPointClasses.First());
                var modInfo = (ModInfoAttribute)mod.GetType().GetCustomAttributes(false).FirstOrDefault(attr => attr is ModInfoAttribute);

                LoadMod(file, modAssembly, mod, modInfo);
            }
        }

        internal void LoadMod(string file, Assembly assembly, Mod mod, ModInfoAttribute modInfo)
        {
            try
            {
                if (modInfo == null || modInfo.name == null)
                {
                    var name = Path.GetFileNameWithoutExtension(file);
                    Debug.LogError("Entry point class of '" + name + "' assembly requires a [ModInfo(name=?)] attribute. Skipping mod.");
                    return;
                }

                if(mods.ContainsKey(modInfo.name))
                {
                    Debug.LogError("Another mod exists with the name '" + modInfo.name + "'! Skipping mod.");
                    return;
                }

                var semlInfo = new SemlInfo(file, assembly, mod, modInfo);

                mods.Add(modInfo.name, semlInfo);
                Debug.Log("Loaded '" + semlInfo.id + "' mod.");

                // Initializing should be done at the end
                mod.Initialize();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
