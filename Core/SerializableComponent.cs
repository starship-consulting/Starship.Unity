using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Core {

    [Serializable]
    public class SerializableComponent {

        [HideInInspector]
        public string TypeName;

        [HideInInspector]
        public string Index;

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
            return Get(behaviour, TypeName, Index);
        }
    }
}
