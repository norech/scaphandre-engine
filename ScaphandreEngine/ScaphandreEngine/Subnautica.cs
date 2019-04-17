using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine
{
    public class Subnautica
    {
        /// <summary>
        /// Returns the datetime of your Subnautica build
        /// </summary>
        public static DateTime BuildTime => SNUtils.GetDateTimeOfBuild();

        /// <summary>
        /// Returns the version of the game in `MM/yyyy` format
        /// </summary>
        public static string Version => string.Format("{0}/{1}", BuildTime.Month, BuildTime.Year);
    }
}
