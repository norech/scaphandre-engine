using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScaphandreEngine.ModLoader
{
    internal class SemlWorker : MonoBehaviour
    {
        public static SemlWorker root;

        private List<Action> tickActions = new List<Action>();

        public void ScheduleOnTick(Action action)
        {
            tickActions.Add(action);
        }

        internal SemlWorker AddChildWorker(string name)
        {
            var child = new GameObject("___SCAPHAN__WKR___;" + name, typeof(SemlWorker));
            child.transform.parent = gameObject.transform;
            return child.GetComponent<SemlWorker>();
        }

        public void Update()
        {
            foreach(var tickAction in tickActions)
            {
                if (tickAction == null) continue;
                tickAction();
            }
        }
    }
}
