using Assets.Scripts.Attributes;
using Assets.Scripts.Core;
using Assets.Scripts.Events.UI;
using Assets.Scripts.Interfaces;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Drag {
    public class DragDropReceiver : BaseComponent, IDragHandler, IPointerEnterHandler, IPointerExitHandler {

        public void OnDrag(PointerEventData e) {
        }

        public void OnPointerEnter(PointerEventData e) {
            Publish(new EnteredDragDropZone(this));
        }

        public void OnPointerExit(PointerEventData e) {
            Publish(new ExitedDragDropZone(this));
        }

        [ValidTypes(typeof(IsTrait))]
        public SerializableType ReceiverType;
    }
}