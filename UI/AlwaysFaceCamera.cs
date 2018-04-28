using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class AlwaysFaceCamera : BaseComponent {

        private void Update() {
            //var viewPosition = Camera.main.WorldToViewportPoint(Target.transform.position);
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }
}