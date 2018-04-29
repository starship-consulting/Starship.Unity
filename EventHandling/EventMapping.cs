using System;
using Starship.Unity.ScriptableObjects;
using UnityEngine;

namespace Starship.Unity.EventHandling {

    [Serializable]
    public struct EventMapping {

        public GameEvent Type;

        public GameObject Action;

        public override string ToString() {
            if(Type == null) {
                return "None";
            }

            if(Action == null) {
                return Type.name;
            }

            return Type.name + " -> " + Action.name;
        }
    }
}