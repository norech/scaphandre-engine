using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Events
{
    public class PlayerDepthClassChangedEvent : Event
    {
        public PlayerDepthClassChangedEvent(Player player, Ocean.DepthClass depthClass)
        {
            Player = player;
            DepthClass = depthClass;
        }

        public Player Player { get; }
        public Ocean.DepthClass DepthClass { get; }

        public override bool IsCancellable => false;
    }
}
