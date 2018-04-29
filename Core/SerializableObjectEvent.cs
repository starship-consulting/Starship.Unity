using System;
using System.Reflection;
using UnityEngine;

namespace Starship.Unity.Core {

    [Serializable]
    public class SerializableObjectEvent {

        public EventInfo GetEvent() {
            if (Behaviour == null) {
                return null;
            }

            return Behaviour.GetType().GetEvent(Event);
        }

        public GameObject Object;

        public MonoBehaviour Behaviour;

        public string Event;
    }
}