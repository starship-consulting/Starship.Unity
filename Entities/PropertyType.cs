using System;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Entities {
    
    [CreateAssetMenu(menuName = "ScriptableObjects/PropertyType")]
    public class PropertyType : BaseScriptableObject {
        public ValueTypes ValueType;
    }

    public enum ValueTypes {
        Integer = 0,
        String,
        GameObject,
        Boolean,
        Sprite
    }
}