using ScaphandreEngine.ModLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Debug = UnityEngine.Debug;

namespace ScaphandreEngine
{
    public class Logger
    {
        private readonly string modName;

        public Logger(Mod mod)
        {
            modName = SemlLoader.GetSemlInfo(mod).id;
        }

        private string Logify(object message)
        {
            return modName + ": " + message.ToString();
        }

        public void Log(object message, UnityEngine.Object context)
        {
            Debug.Log(Logify(message), context);
        }

        public void Log(object message)
        {
            Debug.Log(Logify(message));
        }
        
        public void Error(object message, UnityEngine.Object context)
        {
            Debug.LogError(Logify(message), context);
        }

        public void Error(object message)
        {
            Debug.LogError(Logify(message));
        }

        public void Warning(object message)
        {
            Debug.LogWarning(Logify(message));
        }

        public void Warning(object message, UnityEngine.Object context)
        {
            Debug.LogWarning(Logify(message), context);
        }
    }
}
