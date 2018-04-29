using System;
using System.Reflection;
using UnityEngine;

namespace Starship.Unity.Core {

    [Serializable]
    public class SerializableObjectMethod {

        public MethodInfo GetMethod() {
            return Behaviour != null ? Behaviour.GetType().GetMethod(Method) : null;
        }

        public MonoBehaviour Behaviour;

        public string Method;
    }
}