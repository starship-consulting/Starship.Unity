using System;
using UnityEngine;

namespace Starship.Unity.Utilities {
    public static class Log {

        public static void Write(string text) {
            if (Logged != null) {
                Logged(text);
            }

            Debug.Log(text);
        }

        public static event Action<string> Logged;
    }
}