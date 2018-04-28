using System;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Core {

    [Serializable]
    public class SerializableObjectMethod {

        public MethodInfo GetMethod() {
            return Behaviour != null ? Behaviour.GetType().GetMethod(Method) : null;
        }

        public MonoBehaviour Behaviour;

        public string Method;
    }
}