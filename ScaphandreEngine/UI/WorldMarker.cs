using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine.UI
{
    public class WorldMarker
    {
        public enum MarkerColor
        {
            Default = Blue,

            Blue = 0,
            Orange,
            Red,
            Green,
            Yellow
        }

        /// <summary>
        /// Creates a new marker
        /// </summary>
        /// <param name="position">marker position</param>
        /// <param name="pingType">ping type of the marker, used to retrieve the marker icon</param>
        /// <param name="label">name of the marker</param>
        /// <returns>the created world marker</returns>
        public static WorldMarker Create(Vector3 position, PingType pingType = PingType.Signal, string label = "Marker")
        {
            GameObject pingGO = new GameObject();
            PingInstance ping = pingGO.AddComponent<PingInstance>();

            pingGO.transform.position = position;
            ping.origin = pingGO.transform;
            ping.colorIndex = (int)MarkerColor.Default;
            ping.pingType = pingType;
            ping.SetLabel(label);

            PingManager.Register(ping);

            return new WorldMarker(ping);
        }

        /// <summary>
        /// Destroys a world marker
        /// </summary>
        /// <param name="marker">the marker to destroy</param>
        public static void Destroy(WorldMarker marker)
        {
            PingManager.Unregister(marker.Component);

            GameObject.Destroy(marker.Component.gameObject);
        }

        public PingInstance Component { get; }

        public WorldMarker(PingInstance pingInstance)
        {
            Component = pingInstance;
        }

        /// <summary>
        /// Hides the world marker from player's view. Player can still show it using the Beacon Manager.
        /// </summary>
        public void Hide()
        {
            Component.SetVisible(false);
        }

        /// <summary>
        /// Shows the world marker to player. Player can still hide it using the Beacon Manager.
        /// </summary>
        public void Show()
        {
            Component.SetVisible(true);
        }

        /// <summary>
        /// Marker visibility. Player can change it manually using the Beacon Manager.
        /// </summary>
        public bool Visible
        {
            get => Component.visible;
            set => Component.SetVisible(value);
        }

        /// <summary>
        /// Marker color
        /// </summary>
        public MarkerColor Color
        {
            get => (MarkerColor)Component.colorIndex;
            set => Component.SetColor((int)value);
        }

        /// <summary>
        /// Ping type, used to retrieve the marker icon
        /// </summary>
        public PingType PingType
        {
            get => Component.pingType;
            set => Component.pingType = value;
        }

        /// <summary>
        /// Name of the marker
        /// </summary>
        public string Label
        {
            get => Component.GetLabel();
            set => Component.SetLabel(value);
        }

        /// <summary>
        /// Marker position
        /// </summary>
        public Vector3 Position
        {
            get => Component.origin.position;
            set => Component.origin.position = value;
        }

        /// <summary>
        /// Should we display the marker in the Beacon Manager?
        /// </summary>
        public bool DisplayInManager
        {
            get => Component.displayPingInManager;
            set => Component.displayPingInManager = value;
        }

    }
}
