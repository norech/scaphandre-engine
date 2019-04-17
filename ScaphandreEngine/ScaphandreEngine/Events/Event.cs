using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Events
{
    public abstract class Event
    {
        public bool IsPropagationStopped { get; private set; }

        public void StopPropagation()
        {
            IsPropagationStopped = true;
        }

        public abstract bool IsCancelled { get; set; }
        public abstract bool IsCancelable { get; }
    }
}
