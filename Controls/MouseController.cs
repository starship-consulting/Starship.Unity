using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Enumerations;
using Assets.Scripts.EventHandling;
using Assets.Scripts.EventHandling.Events;
using Assets.Scripts.Events.Interaction;
using Assets.Scripts.Events.Targetting;
using Assets.Scripts.Interaction;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controls {
    public class MouseController : BaseComponent {

        static MouseController() {
            MouseButtonStates = new Dictionary<int, bool>();

            for (var button = 0; button < 2; button++) {
                MouseButtonStates.Add(button, false);
            }
        }

        protected override void Awake() {
            base.Awake();
            //On<TargetChanged>(OnTargetChanged);
        }

        protected override void OnEnable() {
            base.OnEnable();

            LockCursor();
            SetCursor(MainCursor);
        }

        public static void ToggleCursor() {
            Cursor.visible = !Cursor.visible;

            if(Cursor.visible) {
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public static void LockCursor() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            EventHub.Publish(new CursorStateChanged { IsLocked = true });
        }

        public static void FreeCursor() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            EventHub.Publish(new CursorStateChanged { IsLocked = false });
        }

        /*public void ToggleCursor() {
            SetCursorVisibility(!Cursor.visible);
        }*/

        public void SetCursorVisibility(bool isVisible) {
            ShowCursor = Cursor.visible = isVisible;
            Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
        
        /*private void OnTargetChanged(TargetChanged e) {
            if (e.Target == null) {
                SetCursor(MainCursor);
                return;
            }

            var target = e.Target.GetComponent<Targettable>();

            if (target != null) {
                switch (target.Type) {
                    case EntityTypes.Creature:
                        SetCursor(AttackCursor);
                        break;
                    case EntityTypes.Item:
                        SetCursor(ItemCursor);
                        break;
                }
            }
        }*/

        private void SetCursor(Texture2D cursor) {
            //if(ShowCursor) {
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            //}
        }

        private void LateUpdate() {
            for (var button = 0; button < 2; button++) {
                if (Input.GetMouseButtonUp(button)) {
                    MouseButtonStates[button] = false;
                    Publish(new MouseUp((PointerEventData.InputButton) button));
                    MouseStateChanged((MouseButtons)button, false);
                }

                if (Input.GetMouseButtonDown(button)) {
                    MouseButtonStates[button] = true;
                    Publish(new MouseDown((PointerEventData.InputButton) button));
                    MouseStateChanged((MouseButtons)button, true);
                }
            }

            /*var hovering = EventSystem.current.IsPointerOverGameObject();

            if (hovering) {
                MousePointer.Hover();

                if (Input.GetMouseButtonDown(0)) {
                    MousePointer.BeginDragging();
                }
            }
            else {
                MousePointer.Unhover();

                for (var button = 0; button < 2; button++) {
                    if (Input.GetMouseButtonUp(button)) {
                        MouseButton.Get(button).Depress();
                    }

                    if (Input.GetMouseButtonDown(button)) {
                        MouseButton.Get(button).Press();
                    }
                }
            }

            if (!Input.GetMouseButton(0)) {
                MousePointer.EndDragging();
            }*/
        }

        private void MouseStateChanged(MouseButtons button, bool isDown) {
            foreach (var binding in Bindings) {
                if (binding.Button == button && binding.Event != null) {
                    if (isDown && binding.State == ButtonStates.Down) {
                        Publish(new GameEventFired { Event = binding.Event });
                    }
                    else if(!isDown && binding.State == ButtonStates.Up) {
                        Publish(new GameEventFired { Event = binding.Event });
                    }
                }
            }
        }

        public static bool IsMouseButtonDown(MouseButtons button) {
            return MouseButtonStates[(int) button];
        }

        public Texture2D MainCursor;

        public Texture2D AttackCursor;

        public Texture2D ItemCursor;

        public bool ShowCursor = true;
        
        public MouseBinding[] Bindings;
        
        private static Dictionary<int, bool> MouseButtonStates { get; set; }
    }
}