using System;
using UnityEngine;

namespace Assets.Scripts.Utilities {
    public class Benchmark : IDisposable {
        public Benchmark(string message) {
            StartTime = DateTime.Now;
            Message = message;
        }

        public void Dispose() {
            Time = DateTime.Now - StartTime;

            if (!Message.Contains("{0}")) {
                Message = "({0} ms) " + Message;
            }

            Debug.Log(string.Format(Message, Time.TotalMilliseconds));
        }

        public TimeSpan Time { get; set; }

        private string Message { get; set; }

        private DateTime StartTime { get; set; }
    }
}
