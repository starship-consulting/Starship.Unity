using Assets.Scripts.Core;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Debugging {
    public class ConsoleDebugLogger : BaseComponent {
        protected override void Start() {
            base.Start();

            Log.Logged += OnLogged;
        }

        private void OnLogged(string message) {
            Debug.Log(message);
        }
    }
}