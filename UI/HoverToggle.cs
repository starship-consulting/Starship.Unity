using Starship.Unity.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Starship.Unity.UI {
    public class HoverToggle : BaseComponent, IPointerEnterHandler, IPointerExitHandler {

        protected override void OnEnable() {
            base.OnEnable();
            Target.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData e) {
            Target.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData) {
            Target.SetActive(false);
        }

        public GameObject Target;
    }
}