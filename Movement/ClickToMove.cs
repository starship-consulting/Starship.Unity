using Assets.Scripts.Actors;
using Assets.Scripts.Computations;
using Assets.Scripts.Controls;
using Assets.Scripts.Core;
using Assets.Scripts.Effects;
using Assets.Scripts.EventHandling.Events;
using Assets.Scripts.Events.Interaction;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Movement {
    public class ClickToMove : BaseComponent {

        protected override void OnEnable() {
            //Actor = GetComponentInParent<Actor>();
            
            On<MouseDown>(OnMouseDown);
            On<MouseUp>(OnMouseUp);
            //On<TargetSelected>(OnTargetSelected);
        }

        private void OnMouseUp(MouseUp e) {
            if (e.Button != PointerEventData.InputButton.Left) {
                return;
            }

            StopMoving();

            return;
            
            if (Movement != null) {

                StopMoving();

                var speed = Actor.Compute(Speed);
                var raycast = MouseHelper.Raycast();
                var position = raycast.point;
                var range = 0.3f;
                var targettable = raycast.transform.gameObject.GetComponent<Targettable>();

                if (targettable != null) {
                    range = 2;
                }

                //var destination = Instantiate(MovementIndicator);
                //destination.transform.position = new Vector3(position.x, transform.position.y, position.z);

                /*Movement = Actor.Add<MoveToObject>(move => {
                    move.Speed = speed;
                    move.Destination = destination;
                    move.Range = range;
                });

                Movement.AsPromise().Then(() => StopMoving());*/
            }
        }

        private void OnMouseDown(MouseDown e) {
            if (e.Button != PointerEventData.InputButton.Left) {
                return;
            }

            BeginMoving();
        }

        /*private void OnTargetSelected(TargetSelected e) {
            if (e.Target != null) {
                BeginMoving();
            }
        }*/

        private void BeginMoving() {
            StopMoving();

            //var speed = Actor.Compute(Speed);
            var raycast = MouseHelper.Raycast();
            var position = raycast.point;
            Target = raycast.transform.gameObject;
            
            //var distance = Vector3.Distance(new Vector3(raycast.point.x, 0, raycast.point.z), new Vector3(Actor.transform.position.x, 0, Actor.transform.position.z));
            
            //MovementEffect = Actor.AddEffect(RunEffect);

            /*if (Distance > 3) {
                MovementEffect = Actor.AddEffect(RunEffect);
            }
            else {
                MovementEffect = Actor.AddEffect(WalkEffect);
                speed /= 2;
            }*/

            /*if (targettable != null) {
                Movement = Actor.Create<MoveToObject>(cursor => {
                    cursor.Speed = speed;
                    cursor.Target = targettable.gameObject;
                });

                Movement.AsPromise().Then(() => {
                    TryInteract(targettable.gameObject);
                });
            }
            else {
                Movement = Actor.Create<FollowCursor>(cursor => { cursor.Speed = speed; });
            }*/

            if(!Target.HasComponent<Targettable>()) {
                Target = gameObject;
                //target = Instantiate(MovementIndicator);
                //target.transform.position = new Vector3(position.x, transform.position.y, position.z);

                this.GetOrAdd<FollowCursor>();
            }

            /*Movement = Actor.Add<MoveToObject>(move => {
                move.Speed = speed;
                move.Target = target;
            });*/

            Actor.With<MovementController>(controller => {
                controller.SetDestination(Target);
            });

            /*Movement.AsPromise().Then(() => {
                if(IsTemporary) {
                    //target.Delete();
                }
                else {
                    //TryInteract(targettable.gameObject);
                }

                StopMoving();
            });*/
        }
        
        public void TryInteract(GameObject target) {
            /*Publish(new RequestInteract {
                Source = Actor.gameObject,
                Target = target
            });*/
        }

        public void StopMoving() {

            if(Target == gameObject) {
                this.Remove<FollowCursor>();
            }
            /*if(MovementEffect != null) {
                MovementEffect.gameObject.Delete();
                MovementEffect = null;
            }*/
        }
        
        public Actor Actor;

        public Effect WalkEffect;

        public Effect RunEffect;
        
        public Algorithm Speed;
        
        private GameObject Target;

        //private Effect MovementEffect;

        private BaseComponent Movement { get; set; }
    }
}