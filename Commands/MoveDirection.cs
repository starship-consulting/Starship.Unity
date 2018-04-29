using UnityEngine;

namespace Starship.Unity.Commands {
    public class MoveDirection : Command {

        protected override void Start() {
            base.Start();

            StartPosition = transform.position;
            StartDistance = Vector3.Distance(transform.position, Destination);
        }
        
        public override void Undo() {
            Destination = StartPosition;
        }

        protected void FixedUpdate() {
            var distance = Vector3.Distance(transform.position, Destination);
            var percent = 1 - (distance/StartDistance);

            if (distance > 0.1f) {
                transform.position = Vector3.MoveTowards(transform.position, Destination, Time.deltaTime*Speed);
                SetProgress(percent);
                return;
            }

            transform.position = Destination;
            SetProgress(1);
        }

        public float Speed;

        public float StartDistance;

        public Vector3 Destination;

        public Vector3 StartPosition;
    }
}