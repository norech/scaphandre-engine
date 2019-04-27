using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Events
{
    class StoryGoalCompletedEvent : Event
    {
        public StoryGoalCompletedEvent(string key)
        {
            Key = key;
        }

        public string Key { get; }

        public override bool IsCancellable => true;
    }
}
