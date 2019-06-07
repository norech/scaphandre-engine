using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Scheduling
{
    public class Task
    {
        public enum TaskType
        {
            OneShot,
            Repeating
        }

        readonly Action action;

        public Task(Action action, float delay)
        {
            this.action = action;
            Delay = delay;
            Type = TaskType.OneShot;
        }

        public Task(Action action, float delay, float interval)
        {
            this.action = action;
            Delay = delay;
            Interval = interval;
            Type = TaskType.Repeating;
        }

        public bool Cancelled { get; set; }
        public float Delay { get; }
        public float Interval { get; }
        public TaskType Type { get; }

        protected bool alreadyExecuted = false;
        protected float timeSincePrevious = 0;

        internal void Tick()
        {
            timeSincePrevious += UnityEngine.Time.deltaTime;
        }

        internal bool CanExecuteNow => alreadyExecuted ? (timeSincePrevious > Interval) : (timeSincePrevious > Delay);

        internal void Execute()
        {
            alreadyExecuted = true;
            action();

            if (Type == TaskType.OneShot) Cancelled = true;

            timeSincePrevious = 0;
        }

    }
}
