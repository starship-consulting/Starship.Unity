using System;
using System.Collections;
using System.Collections.Generic;
using Starship.Unity.Audio;
using Starship.Unity.Commands;
using Starship.Unity.Controls;
using Starship.Unity.Core;
using Starship.Unity.Debugging;
using Starship.Unity.Enumerations;
using Starship.Unity.Events;
using Starship.Unity.Extensions;
using Starship.Unity.Scheduling;
using Starship.Unity.Utilities;
using UnityEngine;

namespace Starship.Unity.Movement {
    public class GridMovement : BaseComponent, IsMovementController {

        protected override void OnEnable() {
            base.OnEnable();

            if (IsPlayer) {
                On<DungeonReset>(OnDungeonReset);
                On<HotkeyStateChanged>(OnHotkeyStateChanged);
                Commands = new CommandQueue(2);
            }

            StartPosition = transform.position;
            StartRotation = transform.rotation;
        }

        private void OnHotkeyStateChanged(HotkeyStateChanged e) {
            if (e.Hotkey < HotkeyTypes.MoveForward || e.Hotkey > HotkeyTypes.TurnRight) {
                return;
            }

            var direction = GetDirection(e.Hotkey);

            if (e.IsUp) {
                if (direction == RepeatMovement) {
                    RepeatMovement = MovementDirections.None;
                }
            }
            else {
                RepeatMovement = direction;
                AddMove(direction);
            }
        }

        private void AddMove(MovementDirections direction) {
            Commands.Add(() => {
                return Move(direction).Then(() => {
                    if (RepeatMovement != 0) {
                        AddMove(RepeatMovement);
                    }
                });
            });
        }

        private void OnDungeonReset(DungeonReset e) {
            transform.position = StartPosition;
            transform.rotation = StartRotation;
        }

        public List<Vector3> GetValidMoves() {
            var results = new List<Vector3>();

            if (RaycastDirection(MovementDirections.Forward).collider == null) {
                results.Add(GetVectorFromDirection(MovementDirections.Forward));
            }
            if (RaycastDirection(MovementDirections.Back).collider == null) {
                results.Add(GetVectorFromDirection(MovementDirections.Back));
            }
            if (RaycastDirection(MovementDirections.Left).collider == null) {
                results.Add(GetVectorFromDirection(MovementDirections.Left));
            }
            if (RaycastDirection(MovementDirections.Right).collider == null) {
                results.Add(GetVectorFromDirection(MovementDirections.Right));
            }

            return results;
        }

        public MovementDirections GetDirection(HotkeyTypes hotkey) {
            switch (hotkey) {
                case HotkeyTypes.MoveBackward:
                    return MovementDirections.Back;
                case HotkeyTypes.MoveLeft:
                    return MovementDirections.Left;
                case HotkeyTypes.MoveRight:
                    return MovementDirections.Right;
                case HotkeyTypes.TurnLeft:
                    return MovementDirections.TurnLeft;
                case HotkeyTypes.TurnRight:
                    return MovementDirections.TurnRight;
                default:
                    return MovementDirections.Forward;
            }
        }

        public Promise Move(MovementDirections direction) {
            switch (direction) {
                case MovementDirections.TurnRight:
                    return FaceEast();
                case MovementDirections.TurnLeft:
                    return FaceWest();
                default:
                    return Move(GetVectorFromDirection(direction));
            }
        }

        public Promise Move(Vector3 destination) {
            if (!isActiveAndEnabled) {
                return Promise.Empty;
            }

            if (MovementSound != null) {
                Publish(new PlaySound(MovementSound, transform.position));
            }

            var command = Add<MoveDirection>();
            command.Destination = destination;
            command.Speed = Speed;
            return new Promise(command);
        }

        public Vector3 MovementDirectionToTransform(MovementDirections direction) {
            Vector3 vector;

            switch (direction) {
                case MovementDirections.Right:
                    vector = Vector3.right;
                    break;
                case MovementDirections.Back:
                    vector = Vector3.back;
                    break;
                case MovementDirections.Left:
                    vector = Vector3.left;
                    break;
                default:
                    vector = Vector3.forward;
                    break;
            }

            return transform.TransformDirection(vector);
        }

        public Vector3 GetVectorFromDirection(MovementDirections direction) {
            Vector3 vector;

            switch (direction) {
                case MovementDirections.Right:
                    vector = Vector3.right;
                    break;
                case MovementDirections.Back:
                    vector = Vector3.back;
                    break;
                case MovementDirections.Left:
                    vector = Vector3.left;
                    break;
                default:
                    vector = Vector3.forward;
                    break;
            }

            var destination = transform.position;
            destination += transform.TransformDirection(vector)*GridSize;
            destination.x = (float) Math.Round(destination.x, 1)/GridSize*GridSize;
            destination.z = (float) Math.Round(destination.z, 1)/GridSize*GridSize;
            destination.y = transform.position.y;

            return destination;
        }

        protected void OnCollisionEnter(Collision collision) {
            this.With<MoveDirection>(c => c.Undo());

            if (CollisionSound != null) {
                Publish(new PlaySound(CollisionSound, transform.position));
            }
        }

        public Promise Face(Vector3 direction) {
            if (!isActiveAndEnabled) {
                return Promise.Empty;
            }

            var rotation = Quaternion.LookRotation(direction - transform.position);
            var euler = rotation.eulerAngles;
            euler.x = Mathf.Round(euler.x/90)*90;
            euler.y = Mathf.Round(euler.y/90)*90;
            euler.z = Mathf.Round(euler.z/90)*90;

            var face = this.AddComponent<FaceDirection>();
            face.Speed = TurnSpeed;
            face.Destination = rotation;
            face.Destination.eulerAngles = euler;
            return new Promise(face);
        }

        public Promise FaceEast() {
            var face = this.AddComponent<FaceDirection>();
            face.Speed = TurnSpeed;
            face.East();
            return new Promise(face);
        }

        public Promise FaceWest() {
            var face = this.AddComponent<FaceDirection>();
            face.Speed = TurnSpeed;
            face.West();
            return new Promise(face);
        }
        
        public IEnumerator LogDirections() {
            yield return new WaitForFixedUpdate();

            this.RemoveAll<DrawDebugRay>();

            Log.Write("Forward: " + RaycastToString(RaycastDirection(MovementDirections.Forward, true)));
            Log.Write("Back: " + RaycastToString(RaycastDirection(MovementDirections.Back, true)));
            Log.Write("Right: " + RaycastToString(RaycastDirection(MovementDirections.Right, true)));
            Log.Write("Left: " + RaycastToString(RaycastDirection(MovementDirections.Left, true)));
        }

        private string RaycastToString(RaycastHit hit) {
            if (hit.collider == null) {
                return "No hit";
            }

            return "Hit " + hit.collider.name;
        }

        public RaycastHit RaycastDirection(MovementDirections moveDirection, bool debug = false) {
            var start = transform.position;
            var end = MovementDirectionToTransform(moveDirection);

            if (start.y < 2) {
                start.y = 2;
            }

            RaycastHit hit;
            Physics.Raycast(start, end, out hit, GridSize*1.5f, -1, QueryTriggerInteraction.Ignore);

            if (debug) {
                var line = this.AddComponent<DrawDebugRay>();
                line.From = start;
                line.To = end*GridSize*1.5f;
            }

            return hit;
        }

        public bool CanMove(MovementDirections moveDirection) {
            return RaycastDirection(moveDirection).distance > GridSize;
        }

        public float Speed = 7;

        public float GridSize = 2.5f;

        public Sound MovementSound;

        public Sound CollisionSound;

        public int TurnSpeed = 400;

        public bool IsPlayer = false;

        public bool IsMoving;

        public event Action OnFinishedMoving;

        private Vector3 StartPosition;

        private Quaternion StartRotation;

        private MovementDirections RepeatMovement;

        private CommandQueue Commands { get; set; }
    }
}