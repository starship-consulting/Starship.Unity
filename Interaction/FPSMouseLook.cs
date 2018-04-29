using Starship.Unity.Core;
using Starship.Unity.Enumerations;
using UnityEngine;

namespace Starship.Unity.Interaction {
    public class FPSMouseLook : BaseComponent {

        protected override void Start() {
            base.Start();

            if (HideCursor) {
                Cursor.visible = false;
            }
        }

        private void Update() {
            if (axes == RotationAxis.MouseXAndY) {
                var rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X")*sensitivityX;

                rotationY += Input.GetAxis("Mouse Y")*sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxis.MouseX) {
                transform.Rotate(0, Input.GetAxis("Mouse X")*sensitivityX, 0);
            }
            else {
                rotationY += Input.GetAxis("Mouse Y")*sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }

        public RotationAxis axes = RotationAxis.MouseXAndY;

        public float sensitivityX = 5F;

        public float sensitivityY = 5F;

        public float minimumX = -360F;

        public float maximumX = 360F;

        public float minimumY = -60F;

        public float maximumY = 60F;

        public bool HideCursor = true;

        private float rotationY = 0F;
    }
}