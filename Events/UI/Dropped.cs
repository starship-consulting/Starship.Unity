using Starship.Unity.UI.Drag;
using UnityEngine;

namespace Starship.Unity.Events.UI {
    public class Dropped {
        public Draggable Source { get; set; }

        public Vector3 Position { get; set; }

        public DragDropReceiver Receiver { get; set; }
    }
}