using Starship.Unity.Actors;
using Starship.Unity.Animations;
using Starship.Unity.Computations;
using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.Collections;

namespace Starship.Unity.Movement {
    public class MovementController : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();

            Actor = this.FindInParents<Actor>();
            AnimationController = this.FindInParents<MecanimAnimationController>();
        }

        public void SetDestination(GameObject destination) {

            var speed = Actor.Compute(Speed);

            Destination = destination;

            Movement = Add<MoveToObject>(move => {
                move.Speed = speed;
                move.Target = Destination;
            });

            AnimationController.SetState("Run", true);
        }

        public void StopMoving() {
            Destination = null;

            AnimationController.SetState("Run", false);

            if(Movement != null) {
                Movement.Delete();
                Movement = null;
            }

            /*if(MovementEffect != null) {
                MovementEffect.gameObject.Delete();
                MovementEffect = null;
            }*/
        }

        public Algorithm Speed;

        [ReadOnly]
        public GameObject Destination;

        private Actor Actor;

        private MecanimAnimationController AnimationController;

        private MoveToObject Movement;
    }
}