using Assets.Scripts.Enumerations;
using UnityEngine;

namespace Assets.Scripts.Interaction {
    public class ToggleableMouseLook : MonoBehaviour {

        private void Start() {
            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().freezeRotation = true;
        }

        private void Update() {

            return;

            if (Input.GetMouseButtonDown(1)) {
                IsActive = true;
                Cursor.visible = false;
            }
            else if (Input.GetMouseButtonUp(1)) {
                IsActive = false;
                Cursor.visible = true;
                transform.rotation = transform.parent.rotation;
            }

            if (IsActive) {
                if (axes == RotationAxis.MouseXAndY) {
                    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                }
                else if (axes == RotationAxis.MouseX) {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                }
                else {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                }
            }
        }
        
        public RotationAxis axes = RotationAxis.MouseXAndY;

        public float sensitivityX = 15F;

        public float sensitivityY = 15F;

        public float minimumX = -360F;

        public float maximumX = 360F;

        public float minimumY = -60F;

        public float maximumY = 60F;

        private float rotationY = 0F;

        private bool IsActive { get; set; }
    }
}
