using System;
using Starship.Unity.ScriptableObjects;
using UnityEngine;

namespace Starship.Unity.Controls {

    [Serializable]
    public struct KeyboardBinding {

        public KeyCode Key;

        public ButtonStates State;

        public KeyModifiers Modifier;

        public GameEvent Event;

        public override string ToString() {
            if(Event == null) {
                return "None";
            }

            if(Modifier == KeyModifiers.None) {
                return Key + " " + State + " -> " + Event.name;
            }

            return Modifier + " " + Key + " " + State + " -> " + Event.name;
        }
    }
}