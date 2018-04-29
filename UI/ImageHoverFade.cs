using System.Collections.Generic;
using Starship.Unity.Core;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Starship.Unity.UI {
    public class ImageHoverFade : BaseComponent, IPointerEnterHandler, IPointerExitHandler {

        public void OnPointerEnter(PointerEventData e) {
        }

        public void OnPointerExit(PointerEventData e) {
        }

        public List<Image> Images;
    }
}