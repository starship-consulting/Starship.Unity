using System;
using Starship.Unity.Entities;

namespace Starship.Unity.Controls {
    public class MousePointer : Entity {

        public void Move(float x, float y) {
            X = x;
            Y = y;

            if (Moved != null) {
                Moved(this);
            }
        }

        public void Hover() {
            if (IsHovering) {
                return;
            }

            IsHovering = true;

            if (Hovered != null) {
                Hovered(this);
            }
        }

        public void Unhover() {
            if (!IsHovering) {
                return;
            }

            IsHovering = false;

            if (Unhovered != null) {
                Unhovered(this);
            }
        }

        public void BeginDragging() {
            if (IsDragging) {
                return;
            }

            IsDragging = true;

            if (BeginDrag != null) {
                BeginDrag(this);
            }
        }

        public void EndDragging() {
            if (!IsDragging) {
                return;
            }

            IsDragging = false;

            if (EndDrag != null) {
                EndDrag(this);
            }
        }

        public void Select() {
            HasSelection = true;
        }

        public void Unselect() {
            HasSelection = false;
        }

        public bool IsBusy {
            get { return IsDragging || IsHovering; }
        }

        public float X { get; private set; }

        public float Y { get; private set; }

        public bool IsHovering { get; private set; }

        public bool IsDragging { get; private set; }

        public bool HasSelection { get; private set; }

        public static event Action<MousePointer> Hovered;

        public static event Action<MousePointer> Unhovered;

        public static event Action<MousePointer> Moved;

        public static event Action<MousePointer> BeginDrag;

        public static event Action<MousePointer> EndDrag;
    }
}
