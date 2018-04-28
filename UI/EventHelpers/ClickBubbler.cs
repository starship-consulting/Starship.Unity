using Assets.Scripts.Core;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.EventHelpers {
    public class ClickBubbler : BaseComponent, IPointerClickHandler {
        public void OnPointerClick(PointerEventData e) {
            ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, e, ExecuteEvents.pointerClickHandler);
        }
    }
}