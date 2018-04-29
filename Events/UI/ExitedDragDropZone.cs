using Starship.Unity.UI.Drag;

namespace Starship.Unity.Events.UI {
    public class ExitedDragDropZone {
        public ExitedDragDropZone() {
        }

        public ExitedDragDropZone(DragDropReceiver zone) {
            Zone = zone;
        }

        public DragDropReceiver Zone;
    }
}