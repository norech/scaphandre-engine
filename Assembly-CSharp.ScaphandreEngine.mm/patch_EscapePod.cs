
using ScaphandreEngine.EventHandling;
using ScaphandreEngine.Events;

class patch_EscapePod : EscapePod
{
    public extern void orig_RespawnPlayer();
    private new void RespawnPlayer()
    {
        EventManager.TriggerEvent(new PlayerRespawnEvent(Player.main));
        orig_RespawnPlayer();
    }
}
