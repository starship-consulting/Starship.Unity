using Starship.Unity.ScriptableObjects;
using UnityEngine;

namespace Starship.Unity.Entities {
    
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