using UnityEngine;

namespace Starship.Unity.Elements {
    public class ElementType : ScriptableObject {
        public string Name;

        public override string ToString() {
            return Name;
        }
    }
}