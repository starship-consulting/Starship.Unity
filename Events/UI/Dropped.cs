using System;
using Assets.Scripts.UI.Drag;
using UnityEngine;

namespace Assets.Scripts.Events.UI {
    public class Dropped {
        public Draggable Source { get; set; }

        public Vector3 Position { get; set; }

        public DragDropReceiver Receiver { get; set; }
    }
}