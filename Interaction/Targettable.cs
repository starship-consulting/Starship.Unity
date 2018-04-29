using Starship.Unity.Core;
using Starship.Unity.Events;
using Starship.Unity.Events.Models;
using Starship.Unity.Events.Targetting;
using UnityEngine.EventSystems;

namespace Starship.Unity.Interaction {
    
    public class Targettable : BaseComponent, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler, IsComponentObserver {
        
        protected override void OnEnable() {
            base.OnEnable();
            Controller = FindObjectOfType<TargetController>();
        }

        protected override void OnDisable() {
            base.OnDisable();
            Disable();
        }

        protected override void OnDestroy() {
            Disable();
            //this.Remove(Highlighter);
            base.OnDestroy();
        }

        private void Disable() {
            if (IsSelected) {
                IsSelected = false;
                Publish(new TargetSelected(null));
            }

            if (IsHovered) {
                IsHovered = false;
                Publish(new TargetChanged(null));
            }

            //Highlighter.Off();
        }

        public void OnComponentStateChanged(ComponentStateChanged e) {
            if (IsSelected) {
                Publish(new SelectedTargetComponentChanged {
                    Target = this
                });
            }
        }

        public void OnPointerEnter(PointerEventData e) {
            BeginHovering();
        }

        public void OnPointerExit(PointerEventData e) {
            IsHovered = false;

            if (IsSelected && e.button != PointerEventData.InputButton.Left) {
                Deselect();
            }

            if (!IsSelected) {
                //EndHighlighting();
            }

            Publish(new TargetChanged(null));
        }

        public void OnPointerDown(PointerEventData e) {
            Publish(new TargetClicked(this));
        }

        public void OnPointerClick(PointerEventData e) {
            //Publish(new TargetClicked(this));
        }

        public void Select() {
            if (IsSelected || !CanSelect) {
                return;
            }

            IsSelected = true;
            Publish(new TargetSelected(this));

            if (!IsHovered) {
                //BeginHighlighting();
            }
        }

        public void Deselect() {
            IsSelected = false;
            //Highlighter.Off();

            if (IsHovered) {
                //BeginHighlighting();
            }
        }
        
        private void BeginHovering() {
            if (IsHovered) {
                return;
            }

            Publish(new TargetChanged(this));
            IsHovered = true;

            if (IsSelected) {
                return;
            }

            //BeginHighlighting();
        }
        
        public string Tooltip;

        public bool IsSelected;

        public bool IsHovered;

        public bool CanSelect = true;
        
        private TargetController Controller { get; set; }
    }
}