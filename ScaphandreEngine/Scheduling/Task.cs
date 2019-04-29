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
        protected float timeSinceStart = 0;
        protected float timeSinceLastExec = 0;

        internal void Tick()
        {
            timeSinceStart += UnityEngine.Time.deltaTime;
            timeSinceLastExec += UnityEngine.Time.deltaTime;
        }

        internal void ResetTimers()
        {
            timeSinceStart = 0;
            timeSinceLastExec = 0;
        }

        internal bool CanExecuteNow => alreadyExecuted ? timeSinceLastExec > Interval : timeSinceStart > Delay;
        internal void Execute()
        {
            alreadyExecuted = true;
            action();

            if (Type == TaskType.OneShot) Cancelled = true;
        }

    }
}
