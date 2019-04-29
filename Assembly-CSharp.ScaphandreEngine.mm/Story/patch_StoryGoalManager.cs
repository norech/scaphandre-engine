using MonoMod;
using ScaphandreEngine.EventHandling;
using ScaphandreEngine.Events;

namespace Story
{
    class patch_StoryGoalManager : StoryGoalManager
    {
        public extern bool orig_OnGoalComplete(string key);
        private new bool OnGoalComplete(string key)
        {
            if (EventManager.TriggerEvent(new StoryGoalCompletedEvent(key)).IsCancelled)
            {
                return false;
            }

            return orig_OnGoalComplete(key);
        }
    }

}