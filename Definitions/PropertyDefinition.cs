using System;
using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.Definitions {

    [Serializable]
    public struct PropertyDefinition {

        public string Title;

        public PropertyTypes Type;

        public Sprite Icon;

        public string Description;
    }
}