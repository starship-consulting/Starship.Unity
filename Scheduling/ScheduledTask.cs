using System;
using UnityEngine;

namespace Starship.Unity.Scheduling {
    public class ScheduledTask {

        public ScheduledTask() {
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }

        public MonoBehaviour Parent { get; set; }
        
        public TimeSpan Delay { get; set; }

        public Coroutine Coroutine { get; set; }

        public bool IsRepeating { get; set; }
    }
}