using Assets.Scripts.UI.Drag;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Events.Models {
    public struct DragResolution {
        public PointerEventData Data;

        public DragDropReceiver Receiver;
    }
}