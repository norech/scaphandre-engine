
using MonoMod;
using ScaphandreEngine.EventHandling;
using ScaphandreEngine.Events;

class patch_Player : Player
{

    public extern void orig_OnKill(DamageType damageType);
    private new void OnKill(DamageType damageType)
    {
        if (EventManager.TriggerEvent(new PlayerDeathEvent(this, damageType)).IsCancelled)
        {
            liveMixin.AddHealth(1);
            return;
        }
        orig_OnKill(damageType);
    }

    public extern void orig_EquipmentChanged(string slot, InventoryItem item);
    private void EquipmentChanged(string slot, InventoryItem item)
    {
        EventManager.TriggerEvent(new PlayerEquipmentChangedEvent(this, slot, item));
        orig_EquipmentChanged(slot, item);
    }

    public extern void orig_OnPlayerIsUnderwaterChanged(Utils.MonitoredValue<bool> isUnderwater);
    private void OnPlayerIsUnderwaterChanged(Utils.MonitoredValue<bool> isUnderwater)
    {
        if (isUnderwater.value)
        {
            EventManager.TriggerEvent(new PlayerEnterWaterEvent(this));
        }
        else
        {
            EventManager.TriggerEvent(new PlayerExitWaterEvent(this));
        }

        orig_OnPlayerIsUnderwaterChanged(isUnderwater);
    }

    public extern void orig_OnPlayerDepthClassChanged(Utils.MonitoredValue<int> depthClass);
    private void OnPlayerDepthClassChanged(Utils.MonitoredValue<int> depthClass)
    {
        EventManager.TriggerEvent(new PlayerDepthClassChangedEvent(this, (Ocean.DepthClass)depthClass.value));
        orig_OnPlayerDepthClassChanged(depthClass);
    }
}
