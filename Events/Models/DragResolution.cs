using Starship.Unity.UI.Drag;
using UnityEngine.EventSystems;

namespace Starship.Unity.Events.Models {
    public struct DragResolution {
        public PointerEventData Data;

        public DragDropReceiver Receiver;
    }
}