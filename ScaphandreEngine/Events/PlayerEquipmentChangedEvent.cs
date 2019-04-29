using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Events
{
    public class PlayerEquipmentChangedEvent : Event
    {
        public PlayerEquipmentChangedEvent(Player player, string slot, InventoryItem item)
        {
            Player = player;
            Slot = slot;
            Item = item;
        }

        public Player Player { get; }
        public string Slot { get; }
        public InventoryItem Item { get; }

        public override bool IsCancellable => false;
    }
}
