using Starship.Unity.Core;
using UnityEngine.EventSystems;

namespace Starship.Unity.UI.EventHelpers {
    public class ClickBubbler : BaseComponent, IPointerClickHandler {
        public void OnPointerClick(PointerEventData e) {
            ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, e, ExecuteEvents.pointerClickHandler);
        }
    }
}