using Starship.Unity.Core;
using Starship.Unity.EventHandling.Events;
using Starship.Unity.Events.Targetting;
using Starship.Unity.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Starship.Unity.Interaction {
    public class TargetController : BaseComponent, HasContext {
        protected override void OnEnable() {
            base.OnEnable();

            On<TargetSelected>(OnTargetSelected);
            On<TargetClicked>(OnTargetClicked);
            On<MouseUp>(OnMouseUp);
            On<MouseDown>(OnMouseDown);
        }

        public void DeselectTarget() {
            if (CurrentTarget != null) {
                CurrentTarget.Deselect();
                Publish(new TargetSelected(null));
            }
        }

        private void OnMouseDown(MouseDown e) {
            if (UntargetOnMouseup && e.Button == PointerEventData.InputButton.Left) {
                if (CurrentTarget != null && !CurrentTarget.IsHovered) {
                    CurrentTarget.Deselect();
                }
            }
        }

        private void OnMouseUp(MouseUp e) {
            if (UntargetOnMouseup && e.Button == PointerEventData.InputButton.Left) {
                if (CurrentTarget != null && !CurrentTarget.IsHovered) {
                    CurrentTarget.Deselect();
                }
            }
        }

        private void OnTargetClicked(TargetClicked e) {
            e.Target.Select();
        }
        
        private void OnTargetSelected(TargetSelected e) {
            if (e.Target == CurrentTarget) {
                return;
            }

            var target = CurrentTarget;

            if (CurrentTarget != null) {
                CurrentTarget = null;
                target.Deselect();
            }

            CurrentTarget = e.Target;
        }

        public GameObject GetContext() {
            if (CurrentTarget == null) {
                return null;
            }

            return CurrentTarget.gameObject;
        }

        public Targettable CurrentTarget;

        public Color HighlightColor = Color.white;

        public bool UntargetOnMouseup = true;
    }
}