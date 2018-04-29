using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;

namespace Starship.Unity.Utility {
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