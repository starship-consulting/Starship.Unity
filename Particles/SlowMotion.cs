using Starship.Unity.Core;
using UnityEngine;

namespace Starship.Unity.Particles {
    public class SlowMotion : BaseComponent {

        protected override void Start() {
            base.Start();
            Time.timeScale *= Factor;
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            Time.timeScale = 1;
        }
        
        public float Factor = 0.5f;
    }
}