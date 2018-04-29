using System;
using Starship.Unity.Interfaces;
using UnityEngine;

namespace Starship.Unity.Crafting {
    
    [Serializable]
    public class Craft : ScriptableObject, HasIcon {

        public string Name { get; set; }
        
        public Sprite Icon { get; set; }

        public CraftComponent[] Components;

        public Sprite GetIcon() {
            return Icon;
        }

        public override string ToString() {
            return Name;
        }
    }
}