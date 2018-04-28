using System;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
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