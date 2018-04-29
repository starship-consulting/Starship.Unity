using System;
using UnityEngine;

namespace Starship.Unity.Core {

    [Serializable]
    public class SerializableType {

        public SerializableType() {
        }

        public SerializableType(Type type) {
            //Value = type;
            Name = type.AssemblyQualifiedName;
        }
        
        [HideInInspector]
        public string Name;

        public Type GetSerializedType() {
            if (string.IsNullOrEmpty(Name)) {
                return null;
            }

            var type = Type.GetType(Name);

            if (type == null) {
                return null;
            }

            return type;
        }

        /*private Type _value;
        public Type Value {
            get {
                if (_value == null) {
                    _value = Type.GetType(Name);

                    if (_value == null) {
                        Debug.Log("Couldn't load type: " + Name);
                    }
                }

                return _value;
            }
            set {
                if (_value != value) {
                    Name = value.AssemblyQualifiedName;
                    _value = value;
                }
            }
        }*/

        /*public bool HasValidName() {
            return !string.IsNullOrEmpty(Name);
        }*/
    }
}