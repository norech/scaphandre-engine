using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Events
{
    public class PlayerDeathEvent : Event
    {
        public PlayerDeathEvent(Player player, DamageType damageType)
        {
            Player = player;
            DamageType = damageType;
        }

        public Player Player { get; }
        public DamageType DamageType { get; }

        public override bool IsCancellable => true;
    }
}
