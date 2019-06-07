using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine.ModLoader
{
    internal class SemlWorker : MonoBehaviour
    {
        public static SemlWorker main;

        private static List<Action> tickActions = new List<Action>();
        private static List<Action<SemlInfo>> modTickActions = new List<Action<SemlInfo>>();

        public static void ScheduleOnTick(Action action)
        {
            tickActions.Add(action);
        }

        public static void ScheduleOnModTick(Action<SemlInfo> action)
        {
            modTickActions.Add(action);
        }

        public void Update()
        {
            foreach(var tickAction in tickActions)
            {
                if (tickAction == null) continue;
                tickAction();
            }

            foreach (var modTickAction in modTickActions)
            {
                if (modTickAction == null) continue;
                foreach (var pair in SemlLoader.mods)
                {
                    var semlInfo = pair.Value;

                    modTickAction(semlInfo);
                }
            }
        }
    }
}
