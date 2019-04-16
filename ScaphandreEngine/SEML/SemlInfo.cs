
using ScaphandreEngine;
using ScaphandreEngine.Util;
using System.Reflection;

namespace SEML
{
    public class SemlInfo
    {
        internal SemlInfo(string file, Assembly assembly, Mod mod, ModInfoAttribute info)
        {
            id = info.id ?? info.name.ToAlphabetic();

            this.file = file;
            this.assembly = assembly;
            this.mod = mod;
            this.info = info;
        }

        public readonly string file;
        public readonly string id;
        public readonly Assembly assembly;
        public readonly Mod mod;
        public readonly ModInfoAttribute info;
    }
}
