using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI {
    public class Label : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        public void OnPointerEnter(PointerEventData e) {
            if (!string.IsNullOrEmpty(Tooltip) && LabelChanged != null) {
                LabelChanged(Tooltip);
            }
        }

        public void OnPointerExit(PointerEventData e) {
            if (!string.IsNullOrEmpty(Tooltip) && LabelChanged != null) {
                LabelChanged(string.Empty);
            }
        }

        public string Tooltip;

        public static event Action<string> LabelChanged;
    }
}
