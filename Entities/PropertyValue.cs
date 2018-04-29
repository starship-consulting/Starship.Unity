using System;
using UnityEngine;

namespace Starship.Unity.Entities {
    
    [Serializable]
    public class PropertyValue {
        
        public string Name;
        
        public int IntValue;

        public string StringValue;
        
        public GameObject GameObjectValue;
        
        public bool BoolValue;
        
        public Sprite SpriteValue;
    }
}