using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.Collections;

namespace Starship.Unity.Movement {
    public class MoveToObject : BaseComponent {
        protected override void Start() {
            CharacterController = this.FindInParents<CharacterController>();
        }

        protected void FixedUpdate() {
            var destination = Target.transform.position;//new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);

            CharacterController.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), RotationSpeed * Time.deltaTime);

            Distance = Vector3.Distance(destination, transform.position);

            if (Distance <= MinimumDistance) {
                //Delete();
                return;
            }
            
            CharacterController.Move((transform.forward*Speed*Time.deltaTime) + new Vector3(0, CharacterController.isGrounded ? 0 : -10*Time.deltaTime, 0));
        }

        public float Speed;
        
        public float RotationSpeed = 10;

        public GameObject Target;

        public float MinimumDistance = 1f;

        [ReadOnly]
        public float Distance;

        private CharacterController CharacterController { get; set; }
    }
}