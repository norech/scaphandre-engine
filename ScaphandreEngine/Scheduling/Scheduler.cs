﻿using ScaphandreEngine.ModLoader;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine.Scheduling
{
    public class Scheduler
    {
        internal static void OnModTick(SemlInfo semlInfo) => semlInfo.mod.Scheduler.Tick();

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

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return SemlWorker.main.StartCoroutine(routine);
        }

        public void StopCoroutine(Coroutine routine)
        {
            SemlWorker.main.StopCoroutine(routine);
        }

        public void StopCoroutine(IEnumerator routine)
        {
            SemlWorker.main.StopCoroutine(routine);
        }

        internal void Tick()
        {
            var cancelledTasks = new List<Task>();
            foreach (var task in tasks)
            {
                if (task.Cancelled)
                {
                    cancelledTasks.Add(task);
                    continue;
                }

                task.Tick();
                if(task.CanExecuteNow)
                {
                    task.Execute();
                }
            }

            foreach(var task in cancelledTasks)
            {
                tasks.Remove(task);
            }
        }
    }
}
