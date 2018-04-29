using System;
using Starship.Unity.Core;
using Starship.Unity.Tweening;
using UnityEngine;

namespace Starship.Unity.Particles {
    public class FadeMaterial : BaseComponent {

        protected FadeMaterial() {
            if (Runner == null) {
                Runner = new TweenRunner<FloatTween>();
            }

            Runner.Init(this);
        }

        protected override void Start() {
            base.Start();

            var floatTween = new FloatTween {
                duration = 2,
                startFloat = 1,
                targetFloat = Opacity
            };

            floatTween.AddOnChangedCallback(UpdateAlpha);
            floatTween.AddOnFinishCallback(Finished);
            Runner.StartTween(floatTween);
        }

        private void Finished() {
            Destroy(this);
        }

        protected void UpdateAlpha(float alpha) {

            var renderer = GetComponent<Renderer>();
            var childRenderers = GetComponentsInChildren<Renderer>();

            foreach (var child in childRenderers) {
                child.material.SetColor("_Color", new Color(child.material.color.r, child.material.color.g, child.material.color.b, alpha));
            }

            renderer.material.SetColor("_Color", new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha));
        }

        public float Opacity = 0;

        public float FadeAfter = 0;

        [NonSerialized] private TweenRunner<FloatTween> Runner;
    }
}