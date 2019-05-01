using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine.ModLoader
{
    internal class SemlBackgroundWorker : MonoBehaviour
    {
        public static SemlBackgroundWorker main;

        public void Update()
        {
            foreach(var pair in SemlLoader.mods)
            {
                var semlInfo = pair.Value;

                semlInfo.mod.Scheduler.Tick();
            }
        }
    }
}
