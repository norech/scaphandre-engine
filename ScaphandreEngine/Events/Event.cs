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

        private bool _isCancelled = false;
        public virtual bool IsCancelled
        {
            get {
                if (!IsCancellable)
                    return false;

                return _isCancelled;
            }
            set
            {
                if (!IsCancellable)
                    throw new System.InvalidOperationException("Event is currently not cancellable");

                _isCancelled = value;
            }
        }

        public virtual bool IsCancellable => false;
    }
}
