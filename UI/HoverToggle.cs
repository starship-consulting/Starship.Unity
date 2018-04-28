using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI {
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