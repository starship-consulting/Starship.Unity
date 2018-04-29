using Starship.Unity.Core;
using Starship.Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Starship.Unity.UI {

    //[ExecuteInEditMode]
    public class PercentFitter : BaseComponent {

        protected override void Awake() {
            base.Awake();
            Rect = GetComponent<RectTransform>();
            HorizontalFitter = this.AddComponent<AspectRatioFitter>();
            HorizontalFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;

            VerticalFitter = this.Create<AspectRatioFitter>();
            VerticalFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        }

        public void Update() {
            return;

            var width = Width;
            var left = Left;
            
            if (left > 0) {
                //width *= left;
            }

            if (Width > 0) {
                //left -= Width;
                //left += ((Width*Left) / 3 * 2);
            }

            HorizontalFitter.aspectRatio = Camera.main.aspect * width;
            Rect.pivot = new Vector2(left, 0.5f);
        }

        public float Width;

        public float Height;

        public float Left;

        public float Right;

        public float Top;

        public float Bottom;

        private RectTransform Rect { get; set; }

        private AspectRatioFitter HorizontalFitter { get; set; }

        private AspectRatioFitter VerticalFitter { get; set; }
    }
}