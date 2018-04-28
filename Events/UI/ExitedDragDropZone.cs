using System;
using Assets.Scripts.UI.Drag;

namespace Assets.Scripts.Events.UI {
    public class ExitedDragDropZone {
        public ExitedDragDropZone() {
        }

        public ExitedDragDropZone(DragDropReceiver zone) {
            Zone = zone;
        }

        public DragDropReceiver Zone;
    }
}