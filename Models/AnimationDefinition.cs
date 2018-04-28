using System;
using UnityEngine;

namespace Assets.Scripts.Models {

    [Serializable]
    public class AnimationDefinition {

        public string Name;
        
        public Avatar Avatar;
        
        public string SubType;

        public float Speed = 1f;

        public override string ToString() {
            return Name;
        }
    }
}