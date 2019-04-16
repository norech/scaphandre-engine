using System;
using System.IO;
using System.Reflection;

[assembly: AssemblyCopyright("Copyright © Alexis Cheron. 2019")]

[assembly: AssemblyInformationalVersion("v0.0.1.1 for Subnautica dec.-2018")]
[assembly: AssemblyVersion("0.0.1.1")]
[assembly: AssemblyFileVersion("0.0.1.1")]

namespace ScaphandreEngine
{
    class SharedAssemblyInfo
    {
        public static string supportedSubnauticaVersion = "12/2018"; // december 2018

        public static bool IsSupportedVersion(string version)
        {
            return version == supportedSubnauticaVersion;
        }

        public static bool IsSupportedVersion(DateTime version)
        {
            return string.Format("{0}/{1}", version.Month, version.Year) == supportedSubnauticaVersion;
        }
    }
}