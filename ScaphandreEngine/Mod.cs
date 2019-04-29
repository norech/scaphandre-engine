using System.ComponentModel;
using ScaphandreEngine.ModLoader;
using ScaphandreEngine.Scheduling;

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

        private Scheduler scheduler;
        public Scheduler Scheduler
        {
            get
            {
                if (scheduler == null)
                {
                    scheduler = new Scheduler(this);
                }

                return scheduler;
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
