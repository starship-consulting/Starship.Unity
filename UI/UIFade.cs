using System;
using Starship.Unity.Core;
using Starship.Unity.Extensions;
using Starship.Unity.Tweening;
using UnityEngine;

namespace Starship.Unity.UI {
    public class UIFade : BaseComponent {

        protected UIFade() {
            if (Runner == null) {
                Runner = new TweenRunner<FloatTween>();
            }

            Runner.Init(this);
        }

        protected override void Awake() {
            base.Awake();
            CanvasGroup = this.GetOrAdd<CanvasGroup>();
        }

        protected override void Start() {
            base.Start();

            if (Duration <= 0) {
                CanvasGroup.alpha = TargetValue;
                return;
            }

            var floatTween = new FloatTween {
                duration = Duration,
                startFloat = CanvasGroup.alpha,
                targetFloat = TargetValue
            };

            floatTween.AddOnChangedCallback(UpdateAlpha);
            floatTween.AddOnFinishCallback(Finished);
            Runner.StartTween(floatTween);
        }

        private void Finished() {
            if (DestroyObjectWhenFinished) {
                Destroy(gameObject);
            }
            else {
                Destroy(this);
            }
        }

        protected void UpdateAlpha(float alpha) {
            
            CanvasGroup.alpha = alpha;
            
            if (alpha == 0f) {
                CanvasGroup.blocksRaycasts = false;
                CanvasGroup.interactable = false;
            }
        }

        public bool DestroyObjectWhenFinished = false;

        public float TargetValue = 0;

        public float Duration = 0.3f;

        private CanvasGroup CanvasGroup { get; set; }

        [NonSerialized]
        private TweenRunner<FloatTween> Runner;
    }
}