using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Conditions {
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