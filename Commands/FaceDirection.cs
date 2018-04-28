using UnityEngine;

namespace Assets.Scripts.Commands {
    public class FaceDirection : Command {
        
        public void East() {
            Face(1);
        }

        public void West() {
            Face(-1);
        }

        public void Reverse() {
            Face(2);
        }

        public void Rotate(int degrees) {
            var rotation = transform.rotation.eulerAngles;
            rotation = new Vector3(rotation.x, rotation.y + degrees, rotation.z);
            Destination = Quaternion.Euler(rotation);
        }

        private void Face(int direction) {
            var rotation = transform.rotation.eulerAngles;
            rotation = new Vector3(rotation.x, rotation.y + 90 * direction, rotation.z);
            Destination = Quaternion.Euler(rotation);
        }

        protected void Update() {
            var angle = Quaternion.Angle(transform.rotation, Destination);

            if (angle > 0.1f) {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Destination, Speed*Time.deltaTime);
                SetProgress(1 - (angle/90));
                return;
            }

            transform.rotation = Destination;
            SetProgress(1);
        }

        public Quaternion Destination;

        public float Speed;
    }
}