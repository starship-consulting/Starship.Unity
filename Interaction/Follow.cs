using UnityEngine;

namespace Assets.Scripts.Interaction {
    public class Follow : MonoBehaviour {

        private void Start() {
            MouseLook = GetComponent<ToggleableMouseLook>();
        }

        private void Update() {
            if (Target != null) {

                transform.position = new Vector3(
                    X ? Target.transform.position.x : transform.position.x,
                    Y ? Target.transform.position.y : transform.position.y,
                    Z ? Target.transform.position.z : transform.position.z);

                if (Rotate) {
                    transform.rotation = Target.transform.rotation;
                }
            }
        }

        public GameObject Target;

        public bool X = true;

        public bool Y = true;

        public bool Z = true;

        public bool Rotate = true;

        private ToggleableMouseLook MouseLook { get; set; }
    }
}