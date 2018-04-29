using System;
using System.Collections;
using System.Collections.Generic;
using Starship.Unity.Enumerations;
using Starship.Unity.Events;
using Starship.Unity.Events.Models;
using UnityEngine;

namespace Starship.Unity.Scheduling {
    public class TaskScheduler : MonoBehaviour, IsComponentObserver {

        public ScheduledTask Run(MonoBehaviour parent, Action action, TimeSpan delay, bool repeat = false) {
            var task = new ScheduledTask();
            task.Delay = delay;
            task.IsRepeating = repeat;
            task.Parent = parent;
            task.Coroutine = StartCoroutine(RunCallback(task, action));

            Coroutines.Add(parent, task);

            return task;
        }

        public void Stop(ScheduledTask task) {
            if (task != null && Coroutines.ContainsKey(task.Parent)) {
                Coroutines.Remove(task.Parent);
            }
        }

        private IEnumerator RunCallback(ScheduledTask task, Action action) {
            yield return new WaitForSeconds(Convert.ToSingle(task.Delay.TotalSeconds));
            action();

            while (task.IsRepeating) {
                yield return new WaitForSeconds(Convert.ToSingle(task.Delay.TotalSeconds));
                action();
            }

            Stop(task);
        }

        public void OnComponentStateChanged(ComponentStateChanged e) {
            if (e.State == ComponentStates.Destroyed) {
            }
        }

        private readonly Dictionary<MonoBehaviour, ScheduledTask> Coroutines = new Dictionary<MonoBehaviour, ScheduledTask>();
    }
}