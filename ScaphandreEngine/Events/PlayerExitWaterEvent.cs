using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Events
{
    public class PlayerExitWaterEvent : Event
    {
        public PlayerExitWaterEvent(Player player)
        {
            Player = player;
        }

        public Player Player { get; }

        public override bool IsCancellable => false;
    }
}
