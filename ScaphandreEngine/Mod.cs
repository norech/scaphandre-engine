using System.ComponentModel;
using ScaphandreEngine.ModLoader;
using ScaphandreEngine.Scheduling;
using UnityEngine;

namespace ScaphandreEngine
{
    public abstract class Mod
    {
        public static T GetMod<T>() where T : Mod
        {
            var semlInfo = SemlLoader.GetSemlInfo(typeof(T));

            if (semlInfo == null) return null;

            return (T)semlInfo.mod;
        }

        internal void Setup()
        {
            if (Worker == null)
            {
                Worker = SemlWorker.root.AddChildWorker(Info.id);
            }
            else
            {
                Worker.enabled = true;
            }

            Initialize();
        }

        internal SemlWorker Worker { get; set; }

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

        public ModInfoAttribute Info => SemlLoader.GetSemlInfo(this).info;

        public abstract void Initialize();

    }
}
