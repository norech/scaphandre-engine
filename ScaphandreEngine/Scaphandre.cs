using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine
{
    public class Scaphandre
    {

        public static string InformationalVersion => SharedAssemblyInfo.GetScaphandreInformationalVersion();
        public static string Version => SharedAssemblyInfo.GetScaphandreVersion();

        public static bool SupportsGameVersion => SharedAssemblyInfo.IsSupportedVersion(Subnautica.Version);
    }
}
