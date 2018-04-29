using Starship.Unity.Controls;
using Starship.Unity.Core;
using UnityEngine;

namespace Starship.Unity.Movement {
    public class RigidBodyChaseCursor : BaseComponent {

        protected override void OnEnable() {
            Rigidbody = GetComponent<Rigidbody>();
        }

        public void Update() {
            var direction = (MouseHelper.GetWorldPosition() - transform.position).normalized;
            Rigidbody.MovePosition(transform.position + direction * Speed * Time.deltaTime);
        }

        public float Speed;
        
        public float RotationSpeed = 10;

        private Rigidbody Rigidbody { get; set; }
    }
}