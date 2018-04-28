using System;
using System.Linq;
using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.Definitions {

    [Serializable]
    public class PropertyManager : ScriptableObject {

        public PropertyDefinition Get(PropertyTypes type) {
            return Properties.FirstOrDefault(each => each.Type == type);
        }

        public PropertyDefinition[] Properties;
    }
}