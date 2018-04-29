using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Starship.Unity.Conditions {
    public class MeshCondition : BaseComponent {

        protected override void Start() {
            base.Start();

            if (this.FindInParents<MeshRenderer>() != null) {
                HasMesh.Invoke();
            }
            else {
                NoMesh.Invoke();
            }
        }

        public UnityEvent HasMesh;

        public UnityEvent NoMesh;
    }
}