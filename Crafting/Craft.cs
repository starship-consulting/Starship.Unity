using System;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Crafting {
    
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