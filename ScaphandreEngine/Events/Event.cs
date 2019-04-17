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

        public virtual bool IsCancelled
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public virtual bool IsCancellable => false;
    }
}
