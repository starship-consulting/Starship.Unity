using System;
using Assets.Scripts.Definitions;
using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.Components {

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