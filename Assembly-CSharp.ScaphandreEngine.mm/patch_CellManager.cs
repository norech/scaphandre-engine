using MonoMod;
using ScaphandreEngine.EventHandling;
using ScaphandreEngine.Events;
using UnityEngine;

class patch_CellManager : CellManager
{
    [MonoModConstructor]
    public patch_CellManager(LargeWorldStreamer streamer, LargeWorldEntitySpawner spawner)
        : base(streamer, spawner)
    {
        //
    }

    public extern void orig_RegisterEntity(LargeWorldEntity entity);
    private new void RegisterEntity(LargeWorldEntity entity)
    {
        orig_RegisterEntity(entity);
        if(EventManager.TriggerEvent(new EntitySpawnEvent(entity.gameObject)).IsCancelled)
        {
            UnregisterEntity(entity);
        }
    }
}
