using System;
using Starship.Unity.Enumerations;
using UnityEngine;

namespace Starship.Unity.Definitions {

    [Serializable]
    public struct PropertyDefinition {

        public string Title;

        public PropertyTypes Type;

        public Sprite Icon;

        public string Description;
    }
}