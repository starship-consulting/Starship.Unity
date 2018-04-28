using System;
using System.Reflection;

namespace Assets.Scripts.Core {

    [Serializable]
    public class SerializableMethod {

        public SerializableMethod() {
        }

        public SerializableMethod(Type type) {
            Type = new SerializableType(type);
        }

        public MethodInfo GetMethod() {
            if (Type != null && Type.GetSerializedType() != null) {
                return Type.GetSerializedType().GetMethod(MethodName);
            }

            return null;
        }

        public SerializableType Type;

        public string MethodName;
    }
}