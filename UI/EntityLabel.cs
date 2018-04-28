using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class EntityLabel : BaseComponent {

        protected override void OnEnable() {
            base.OnEnable();
            Rect = GetComponent<RectTransform>();
        }

        private void LateUpdate() {
            var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, Target.transform.position);
            var position = screenPoint - Rect.sizeDelta/2f; //canvasRectT

            position.y += 30;

            Rect.anchoredPosition = position;
        }

        public GameObject Target;

        private RectTransform Rect { get; set; }
    }
}