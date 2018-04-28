using UnityEngine;

namespace Assets.Scripts.Elements {
    public class ElementType : ScriptableObject {
        public string Name;

        public override string ToString() {
            return Name;
        }
    }
}