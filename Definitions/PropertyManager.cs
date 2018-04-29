using System;
using System.Linq;
using Starship.Unity.Enumerations;
using UnityEngine;

namespace Starship.Unity.Definitions {

    [Serializable]
    public class PropertyManager : ScriptableObject {

        public PropertyDefinition Get(PropertyTypes type) {
            return Properties.FirstOrDefault(each => each.Type == type);
        }

        public PropertyDefinition[] Properties;
    }
}