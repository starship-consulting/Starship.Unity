using Starship.Unity.Attributes;
using Starship.Unity.Core;
using Starship.Unity.Events.UI;
using Starship.Unity.Interfaces;
using UnityEngine.EventSystems;

namespace Starship.Unity.UI.Drag {
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