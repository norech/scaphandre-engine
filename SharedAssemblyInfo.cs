using System;
using System.IO;
using System.Linq;
using System.Reflection;

[assembly: AssemblyCopyright("Copyright © Alexis Cheron. 2019")]

[assembly: AssemblyInformationalVersion("v0.0.1.1 for Subnautica dec.-2018")]
[assembly: AssemblyVersion("0.0.1.1")]
[assembly: AssemblyFileVersion("0.0.1.1")]

namespace ScaphandreEngine
{
    class SharedAssemblyInfo
    {
        private static string _informationalVersion = null;
        private static string _version = null;

        public static string supportedSubnauticaVersion = "12/2018"; // december 2018

        public static bool isStableRelease = false;

        public static bool IsSupportedVersion(string version)
        {
            return version == supportedSubnauticaVersion;
        }

        public static bool IsSupportedVersion(DateTime version)
        {
            return string.Format("{0}/{1}", version.Month, version.Year) == supportedSubnauticaVersion;
        }

        /// <summary>
        /// Returns Scaphandre informational version.
        /// Unsupported in Assembly-CSharp.ScaphandreEngine.mm, use Scaphandre.InformationalVersion instead.
        /// </summary>
        public static string GetScaphandreInformationalVersion()
        {
            if(_informationalVersion == null)
            {
                var attribute = Assembly.GetAssembly(typeof(SharedAssemblyInfo))
                  .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true)
                  .LastOrDefault();
                _informationalVersion = ((AssemblyInformationalVersionAttribute)attribute).InformationalVersion;
            }

            return _informationalVersion;
        }

        /// <summary>
        /// Returns Scaphandre version.
        /// Unsupported in Assembly-CSharp.ScaphandreEngine.mm, use Scaphandre.Version instead.
        /// </summary>
        public static string GetScaphandreVersion()
        {
            if(_version == null)
            {
                var attribute = Assembly.GetAssembly(typeof(SharedAssemblyInfo))
                  .GetCustomAttributes(typeof(AssemblyVersionAttribute), true)
                  .LastOrDefault();
                _version = ((AssemblyVersionAttribute)attribute).Version;
            }

            return _version;
        }
    }
}