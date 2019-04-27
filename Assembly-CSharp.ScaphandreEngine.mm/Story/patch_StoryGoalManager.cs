using MonoMod;
using ScaphandreEngine.EventHandling;
using ScaphandreEngine.Events;

namespace Story
{
    class patch_StoryGoalManager : StoryGoalManager
    {
        public extern bool origin_OnGoalComplete(string key);

        [MonoModReplace]
        private new bool OnGoalComplete(string key)
        {
            if(EventManager.TriggerEvent(new StoryGoalCompletedEvent(key)).IsCancelled) {
                return false;
            }

            return origin_OnGoalComplete(key);
        }
    }

}