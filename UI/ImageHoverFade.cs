using System.Collections.Generic;
using Assets.Scripts.Core;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI {
    public class ImageHoverFade : BaseComponent, IPointerEnterHandler, IPointerExitHandler {

        public void OnPointerEnter(PointerEventData e) {
        }

        public void OnPointerExit(PointerEventData e) {
        }

        public List<Image> Images;
    }
}