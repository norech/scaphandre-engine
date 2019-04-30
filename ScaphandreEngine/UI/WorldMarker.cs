using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine.UI
{
    public class WorldMarker
    {
        public static PingInstance AddPing(Vector3 position, PingType pingType = PingType.Signal, string label = "Marker")
        {
            GameObject pingGO = new GameObject();
            PingInstance ping = pingGO.AddComponent<PingInstance>();

            pingGO.transform.position = position;
            ping.origin = pingGO.transform;
            ping.colorIndex = 0;
            ping.pingType = pingType;
            ping.SetLabel(label);

            PingManager.Register(ping);

            return ping;
        }

        public static void RemovePing(PingInstance ping)
        {
            PingManager.Unregister(ping);

            GameObject.Destroy(ping.gameObject);
        }
    }
}
