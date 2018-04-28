using UnityEngine;

namespace Assets.Scripts.Elements {
    public class Recipe : Element {
        
        public void UpdateData() {
        }

        public override GameObject CreateTemplate() {
            return Primary.CreateTemplate();
        }

        public override void ApplyTo(GameObject instance) {
            Primary.ApplyTo(instance);
            Secondary.ApplyTo(instance);
        }

        public Element Primary;

        public Element Secondary;
    }
}