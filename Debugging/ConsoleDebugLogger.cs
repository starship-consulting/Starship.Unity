using Starship.Unity.Core;
using Starship.Unity.Utilities;
using UnityEngine;

namespace Starship.Unity.Debugging {
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