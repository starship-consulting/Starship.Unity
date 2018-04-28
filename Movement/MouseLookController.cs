using System.Collections.Generic;
using Assets.Scripts.Actors;
using Assets.Scripts.Controls;
using Assets.Scripts.Core;
using Assets.Scripts.Events.Interaction;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interaction;
using UnityEngine;

namespace Assets.Scripts.Movement {
    public class MouseLookController : BaseComponent {

        protected override void Start() {
            base.Start();

            InitializeHeadCamera();
        }

        protected override void OnEnable() {
            base.OnEnable();

            On<CursorStateChanged>(OnCursorStateChanged);
        }

        private void OnCursorStateChanged(CursorStateChanged e) {
            if (PlayerCamera != null) {
                if(e.IsLocked) {
                    PlayerCamera.allowRotation = true;
                }
                else {
                    PlayerCamera.allowRotation = false;
                }
            }
        }

        public void EnableMouseLook() {
            if (PlayerCamera != null) {
                MouseController.LockCursor();
            }
        }

        public void DisableMouseLook() {
            if (PlayerCamera != null) {
                MouseController.FreeCursor();
            }
        }

        public void Toggle() {
            if (PlayerCamera != null) {
                PlayerCamera.allowRotation = !PlayerCamera.allowRotation;
                MouseController.ToggleCursor();
            }
        }
        
        private void InitializeHeadCamera() {
            gameObject.layer = 2; // Ignore raycast

            var actor = this.FindInParents<Actor>();

            var character = actor.GetOrAdd<CharacterController>();
            character.center = new Vector3(0, actor.Height/2, 0);
            character.height = actor.Height;

            PlayerMotor = actor.GetOrAdd<PlayerMotorBehavior>();

            PlayerMotor.footstepTypes = new List<FootstepType> {
                new FootstepType {
                    tagName = "Untagged",
                    volume = 4,
                    sounds = actor.FootstepSounds
                }
            }.ToArray();
            
            if (CameraAttachLocation) {
                Camera.main.transform.transform.SetParent(CameraAttachLocation.transform, false);
            }

            PlayerCamera = actor.GetOrAdd<PlayerCameraBehavior>();
            PlayerCamera.headTransform = Camera.main.transform;

            /*if (CameraBodypart.Length > 0) {
                var bodypart = actor.GetComponentsInChildren<BodypartIdentifier>().FirstOrDefault(each => each.Name.ToLower() == CameraBodypart.ToLower());

                if (bodypart != null) {
                    PlayerCamera.headTransform = bodypart.transform;
                    Camera.main.transform.transform.SetParent(bodypart.transform, false);
                }
            }*/
            
            PlayerCamera.playerCamera = Camera.main;
            PlayerCamera.useHeadBob = false;
            PlayerCamera.mouseSmoothScale = 0;
            PlayerCamera.allowRotation = EnabledAtStart;

            if(EnabledAtStart) {
                MouseController.LockCursor();
            }
            else {
                MouseController.FreeCursor();
            }
        }

        public bool EnabledAtStart;

        public GameObject CameraAttachLocation;

        private PlayerCameraBehavior PlayerCamera { get; set; }

        private PlayerMotorBehavior PlayerMotor { get; set; }
    }
}