using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Utility {
    public class CreateGameObject : BaseComponent {

        protected override void Start() {
            base.Start();

            if (Object != null) {
                this.Create(Object);
            }
        }

        public GameObject Object;
    }
}