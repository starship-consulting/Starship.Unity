using System;
using System.Reflection;
using Starship.Unity.Core;
using UnityEngine;

namespace Starship.Unity.Models {

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