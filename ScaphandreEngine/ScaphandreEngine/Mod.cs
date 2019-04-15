using ScaphandreEngine.ScaphandreEngine;
using SEML;
using System.ComponentModel;

namespace ScaphandreEngine
{
    public abstract class Mod
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Setup()
        {
        }

        private Logger logger;
        public Logger Logger
        {
            get
            {
                if(logger == null)
                {
                    logger = new Logger(this);
                }

                return logger;
            }
        }

        public ModInfoAttribute Info
        {
            get
            {
                return SemlLoader.GetSemlInfo(this).info;
            }
        }

        public abstract void Initialize();

        public static T GetMod<T>() where T : Mod
        {
            var semlInfo = SemlLoader.GetSemlInfo(typeof(T));

            if (semlInfo == null) return null;

            return (T)semlInfo.mod;
        }

    }
}
