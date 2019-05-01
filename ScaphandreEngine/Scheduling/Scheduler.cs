using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.Scheduling
{
    public class Scheduler
    {
        private Mod mod;
        private List<Task> tasks = new List<Task>();

        internal Scheduler(Mod mod)
        {
            this.mod = mod;
        }

        public Task ScheduleDelayed(Action action, float delay)
        {
            Task task = new Task(action, delay);
            tasks.Add(task);

            return task;
        }

        public Task ScheduleRepeated(Action action, float delay, float interval)
        {
            Task task = new Task(action, delay, interval);
            tasks.Add(task);

            return task;
        }

        internal void Tick()
        {
            var toRemove = new List<Task>();
            foreach (var task in tasks)
            {
                if (task.Cancelled)
                {
                    toRemove.Add(task);
                    continue;
                }

                task.Tick();
                if(task.CanExecuteNow)
                {
                    task.Execute();
                    task.ResetTimers();
                }
            }

            foreach(var task in toRemove)
            {
                tasks.Remove(task);
            }
        }
    }
}
