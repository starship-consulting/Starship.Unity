using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Effects {
    public class Effect : BaseComponent {

        public string Name;

        public Sprite Icon;

        public override string ToString() {
            return Name;
        }
    }
}