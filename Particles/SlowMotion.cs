using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Particles {
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