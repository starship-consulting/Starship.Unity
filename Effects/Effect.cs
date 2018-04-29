using Starship.Unity.Core;
using UnityEngine;

namespace Starship.Unity.Effects {
    public class Effect : BaseComponent {

        public string Name;

        public Sprite Icon;

        public override string ToString() {
            return Name;
        }
    }
}