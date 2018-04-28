using Assets.Scripts.Controls;
using Assets.Scripts.Interaction;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.EventHandling.Events {
    public class KeyPressed : Event {

        public KeyPressed() {
        }

        public KeyPressed(KeyStatus key) {
            KeyCode = key.KeyCode;
            Modifiers = key.Modifiers;
            Status = key.Status;
        }

        public KeyCode KeyCode { get; set; }

        public KeyModifiers Modifiers { get; set; }

        public KeyStatuses Status { get; set; }
    }
}