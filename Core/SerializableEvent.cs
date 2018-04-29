using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Starship.Unity.Core {

    [Serializable]
    public class SerializableEvent {

        public GameObject Source;

        public string ComponentName;

        public string Index;

        public string EventName;

        public static Component Get(MonoBehaviour behaviour, string typeName, string assignedIndex) {
            return Get(behaviour.gameObject, typeName, assignedIndex);
        }

        public static Component Get(GameObject obj, string typeName, string assignedIndex) {
            if (string.IsNullOrEmpty(typeName)) {
                return null;
            }

            var type = Type.GetType(typeName.Split('|').First());

            if (type == null) {
                return null;
            }

            var index = 0;

            if (!string.IsNullOrEmpty(assignedIndex)) {
                index = int.Parse(assignedIndex);
            }

            return obj.GetComponents(type).Skip(index).FirstOrDefault();
        }

        public Component Get(MonoBehaviour behaviour) {
            return Get(behaviour, ComponentName, Index);
        }

        public Component GetComponent() {
            return Get(Source, ComponentName, Index);
        }

        public EventInfo GetEvent() {
            if (Source != null) {
                var component = GetComponent();

                if (component != null) {
                    return component.GetType().GetEvent(EventName);
                }
            }

            return null;
        }
    }
}
