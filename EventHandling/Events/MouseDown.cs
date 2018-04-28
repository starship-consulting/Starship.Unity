using Assets.Scripts.Controls;
using Assets.Scripts.Interaction;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.EventHandling.Events {
    public class MouseDown : Event, IsSignal {

        public MouseDown() {
            WorldPosition = MouseHelper.Raycast().point;
        }

        public MouseDown(PointerEventData.InputButton button) : this() {
            Button = button;
        }
        
        public PointerEventData.InputButton Button { get; set; }

        public Vector3 WorldPosition { get; set; }
    }
}