using System;
using Starship.Unity.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Starship.Unity.Interaction {
    public class ApplyForce : BaseComponent {
        
        protected override void Start() {
            transform.position = new Vector3(transform.position.x, 2f, transform.position.z);

            var strength = Power;
            var baseStrength = 20;

            if (strength <= 0) {
                var percent = Input.mousePosition.y / Screen.height;
                strength = Math.Max((baseStrength * percent) + 1, 10) + Random.Range(-5, 5);
            }

            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, 20);
            
            var direction = (hit.point - transform.position);
            direction.Normalize();

            GetComponent<Rigidbody>().AddForce(direction*strength, Force);
        }

        public ForceMode Force = ForceMode.Impulse;

        public float Power = 0;
    }
}