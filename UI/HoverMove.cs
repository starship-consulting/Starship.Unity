using System;
using Starship.Unity.Core;
using Starship.Unity.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Starship.Unity.UI {
    public class HoverMove : BaseComponent, IPointerEnterHandler, IPointerExitHandler {

        protected HoverMove() {
            if (Runner == null) {
                Runner = new TweenRunner<FloatTween>();
            }

            Runner.Init(this);
        }

        protected override void Awake() {
            base.Awake();
            Rect = GetComponent<RectTransform>();
        }

        protected override void Start()  {
            base.Start();

            OriginalPosition = Rect.anchoredPosition;
        }

        public void OnPointerEnter(PointerEventData e) {
            var floatTween = new FloatTween {
                duration = Duration,
                startFloat = Rect.anchoredPosition.y,
                targetFloat = OriginalPosition.y + Y
            };

            floatTween.AddOnChangedCallback(UpdateYPosition);
            Runner.StartTween(floatTween);
        }

        public void OnPointerExit(PointerEventData e) {
            
            var floatTween = new FloatTween {
                duration = Duration,
                startFloat = Rect.anchoredPosition.y,
                targetFloat = OriginalPosition.y
            };

            floatTween.AddOnChangedCallback(UpdateYPosition);
            Runner.StartTween(floatTween);
        }

        protected void UpdateYPosition(float y) {
            Rect.anchoredPosition = new Vector2(Rect.anchoredPosition.x, y);
        }


        public float X;

        public float Y;

        public float Duration = 0.5f;
        
        [NonSerialized]
        private TweenRunner<FloatTween> Runner;

        private Vector2 OriginalPosition { get; set; }

        private RectTransform Rect { get; set; }
    }
}