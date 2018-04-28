using System;
using System.Reflection;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Models {

    [Serializable]
    public class BehaviourBinding {

        public SerializableComponent Component;

        public string PropertyName;

        public PropertyInfo GetProperty(MonoBehaviour behaviour) {
            var component = Component.Get(behaviour);
            return component.GetType().GetProperty(PropertyName);
        }
    }
}