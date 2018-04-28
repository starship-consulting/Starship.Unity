using System;
using Assets.Scripts.UI.Drag;

namespace Assets.Scripts.Events.UI {
    public class EnteredDragDropZone {

        public EnteredDragDropZone() {
        }

        public EnteredDragDropZone(DragDropReceiver zone) {
            Zone = zone;
        }

        public DragDropReceiver Zone;
    }
}