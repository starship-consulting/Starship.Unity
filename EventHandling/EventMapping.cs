using System;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.EventHandling {

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