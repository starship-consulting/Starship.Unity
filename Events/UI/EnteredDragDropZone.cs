using Starship.Unity.UI.Drag;

namespace Starship.Unity.Events.UI {
    public class EnteredDragDropZone {

        public EnteredDragDropZone() {
        }

        public EnteredDragDropZone(DragDropReceiver zone) {
            Zone = zone;
        }

        public DragDropReceiver Zone;
    }
}