using System;
using Assets.Scripts.Actors;
using Assets.Scripts.Animations;
using Assets.Scripts.Commands;
using Assets.Scripts.Components;
using Assets.Scripts.Enumerations;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Models;
using Assets.Scripts.Extensions;
using Assets.Scripts.Movement;

namespace Assets.Scripts.Entities {
    public class Creature : Actor, IsComponentObserver {

        protected override void Awake() {
            base.Awake();
            //base.Awake();
            
            /*var target = this.GetOrAdd<Targettable>();
            target.Tooltip = Tooltip;
            target.Type = EntityTypes.Creature;
            target.CanSelect = true;

            Grid = GetComponent<GridMovement>();
            AnimationController = GetComponent<LegacyCreatureAnimationController>();*/
            
            //float y = GetComponent<BoxCollider>().size.y;
            //var text = this.Create<FloatingText>();
            //text.transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public void OnComponentStateChanged(ComponentStateChanged e) {
            /*var move = e.Component as MoveDirection;

            if (move != null) {
                if (e.State == ComponentStates.Started) {
                    AnimationController.Play(CreatureAnimationStates.Run);
                    move.ProgressChanged += OnReadyIdle;
                }
            }
            else if (e.Component is FaceDirection) {
                if (e.State == ComponentStates.Started) {
                    AnimationController.Play(CreatureAnimationStates.Walk);
                }
                else if (e.State == ComponentStates.Destroyed) {
                    AnimationController.Play(CreatureAnimationStates.Idle);
                }
            }*/
        }

        private void OnReadyIdle(Command command, float progress) {
            /*if (progress >= 0.7) {
                command.ProgressChanged -= OnReadyIdle;
                AnimationController.Play(CreatureAnimationStates.Idle);
            }*/
        }

        /*protected override void Start() {
            base.Start();
            
            Run(() => {
                transform.position = new Vector3(-6, 0, 3);
                AnimationController.Play(CreatureAnimationStates.Walking);

                Grid.Move(MovementDirections.TurnLeft)
                .Then(() => AnimationController.Play(CreatureAnimationStates.Running))
                .Then(() => Grid.Move(MovementDirections.Forward))
                .Then(() => Grid.Move(MovementDirections.Forward))
                .When(progress => progress >= 0.7, () => {
                    AnimationController.Play(CreatureAnimationStates.Idle);
                    return Grid.Move(MovementDirections.TurnRight);
                });

            });//, TimeSpan.FromSeconds(3));
        }*/
        
        private LegacyCreatureAnimationController AnimationController { get; set; }

        private GridMovement Grid { get; set; }
    }
}