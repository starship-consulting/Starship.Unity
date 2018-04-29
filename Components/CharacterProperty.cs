using System;
using Starship.Unity.Definitions;
using Starship.Unity.Enumerations;
using UnityEngine;

namespace Starship.Unity.Components {

    [Serializable]
    public class CharacterProperty {

        public Sprite GetIcon(PropertyManager manager) {
            return manager.Get(Type).Icon;
        }

        public bool IsResist { get { return (int)Type > 99 && (int)Type < 150; } }

        public bool IsStat { get { return (int)Type > 0 && (int)Type < 50; } }

        public PropertyTypes Type;

        public int Value;
    }
}