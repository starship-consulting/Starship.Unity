using System;
using Starship.Unity.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class ConsoleView : MonoBehaviour {

        private void Awake() {
            Log.Logged += Write;
            Write("Game engine initialized.");
        }

        private void OnDestroy() {
            Log.Logged -= Write;
        }

        public void Write(string text) {
            TextOutput.text += "(" + DateTime.Now.ToShortTimeString() + ") " + text + Environment.NewLine;
        }

        public Text TextOutput;
    }
}