using System;
using Assets.Scripts.Components;
using Assets.Scripts.Entities;
using Assets.Scripts.Enumerations;

namespace Assets.Scripts.Controls {
    public class MouseButton : Entity {

        public MouseButton() {
        }

        public MouseButton(MouseButtons button) {
            Button = button;
        }

        public static MouseButton Get(int button) {
            return Database.Find<MouseButton>(each => (int)each.Button == button);
        }

        public void Press() {
            if (IsDown) {
                return;
            }

            IsDown = true;

            if (Down != null) {
                Down(this);
            }
        }

        public void Depress() {
            if (!IsDown) {
                return;
            }

            IsDown = false;

            if (Up != null) {
                Up(this);
            }
        }

        public MouseButtons Button { get; private set; }

        public bool IsDown { get; private set; }

        public static event Action<MouseButton> Down;

        public static event Action<MouseButton> Up;
    }
}