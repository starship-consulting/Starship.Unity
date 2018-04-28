using Assets.Scripts.Controls;
using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interaction;
using UnityEngine;

namespace Assets.Scripts.Movement {
    public class OldFollowCursor : BaseComponent {
        protected override void Start() {
            CharacterController = this.FindInParents<CharacterController>();
        }

        protected void FixedUpdate() {
            var raycast = MouseHelper.Raycast();
            /*var targettable = raycast.transform.gameObject.GetComponent<Targettable>();

            var distance = 0.3f;

            if (targettable != null) {
                distance = 3;
            }*/

            var destination = raycast.point;
            
            destination.y = transform.position.y;
            Distance = Vector3.Distance(destination, transform.position);

            if (Distance <= MinimumDistance) {
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), RotationSpeed*Time.deltaTime);
            CharacterController.Move((transform.forward*Speed*Time.deltaTime) + new Vector3(0, CharacterController.isGrounded ? 0 : -10*Time.deltaTime, 0));
        }

        public float Speed;
        
        public float Distance;

        public float RotationSpeed = 10;

        public float MinimumDistance = 0.3f;

        private CharacterController CharacterController { get; set; }
    }
}