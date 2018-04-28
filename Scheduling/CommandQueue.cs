using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Scheduling {
    public class CommandQueue {

        public CommandQueue(int maxCommands = 0, bool runConcurrently = false) {
            MaxCommands = maxCommands;
            RunConcurrently = runConcurrently;
        }

        public Promise Add(Func<Promise> action) {
            return Add(new Promise(action));
        }

        public Promise Add(Promise promise) {
            if (RunConcurrently) {
                promise.Run();
                return promise;
            }

            if (MaxCommands != 0 && TotalCommands >= MaxCommands) {
                return Promise.Empty;
            }
            
            TotalCommands += 1;
            Queue.Enqueue(promise);
            Run();

            return promise;
        }
        
        private void Run() {
            if (Current == null && Queue.Any()) {
                Current = Queue.Dequeue();
                Current.Finished += OnFinished;
                Current.Run();
            }
        }

        private void OnFinished(Promise promise) {
            Current.Finished -= OnFinished;
            Current = null;
            TotalCommands -= 1;
            Run();
        }

        public bool RunConcurrently;

        public int MaxCommands;

        public int MaxHistory;
        
        public int TotalCommands;

        private Promise Current { get; set; }

        private readonly Queue<Promise> Queue = new Queue<Promise>();
    }
}